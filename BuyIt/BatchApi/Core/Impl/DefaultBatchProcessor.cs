using BatchApi.Core.Models;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Net;

namespace BatchApi.Core.Impl;

public class DefaultBatchProcessor(
    IBatchExecutorFactory factory,
    ILogger<DefaultBatchProcessor> logger) : IBatchProcessor
{
    private readonly List<BatchRequestItem> _requests = [];

    public void Add(BatchRequestItem definition)
    {
        _requests.Add(definition);
    }

    public async Task<BatchReply> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var responses = new ConcurrentBag<BatchReponse>();

        var options = new ParallelOptions()
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount * 2,
            CancellationToken = cancellationToken,
        };

        await Parallel.ForEachAsync(_requests, options, async (request, ct) =>
        {
            try
            {
                var executor = factory.Create(request);
                var response = await executor.ExecuteAsync(request, ct);
                responses.Add(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An internal error happened.");

                responses.Add(new BatchReponse(
                    request.RequestId,
                    HttpStatusCode.InternalServerError,
                    _emptyHeaders,
                    "An internal error happened."));
            }
        });

        return new(responses);
    }

    private static readonly ImmutableDictionary<string, string> _emptyHeaders =
        ImmutableDictionary.Create<string, string>();
}
