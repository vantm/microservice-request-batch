using BatchApi.Core.Models;
using System.Collections.Immutable;
using System.Net;

namespace BatchApi.Core.Impl;

public class UnsupportedBatchExecutor(Guid requestId) : IBatchExecutor
{
    public ValueTask<BatchReponse> ExecuteAsync(
        BatchRequestItem request, CancellationToken cancellationToken = default)
    {
        var response = new BatchReponse(
            requestId, HttpStatusCode.NotFound, _emptyHeaders, "The request is not found.");
        return ValueTask.FromResult(response);
    }

    private static readonly ImmutableDictionary<string, string> _emptyHeaders =
        ImmutableDictionary.Create<string, string>();
}
