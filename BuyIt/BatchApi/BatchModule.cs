using BatchApi.Core;
using BatchApi.Core.Models;
using Carter;

namespace BatchApi;

public class BatchModule : CarterModule
{
    private const int MaxRequestPerBatch = 50;

    private static readonly IDictionary<string, string[]> TooManyRequestError = new Dictionary<string, string[]>
    {
        [nameof(BatchRequest.Requests)] = ["Too many requests. The maximum is " + MaxRequestPerBatch]
    };

    public BatchModule() : base("/batch")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("", async (
            BatchRequest request, IBatchProcessor processor, CancellationToken ct) =>
        {
            if (request.Requests.Count() > MaxRequestPerBatch)
            {
                return Results.ValidationProblem(TooManyRequestError);
            }

            if (!request.Requests.Any())
            {
                return Results.Ok(BatchReply.Empty);
            }

            processor.AddRange(request.Requests);

            var reply = await processor.ExecuteAsync(ct);

            return Results.Ok(reply);
        });
    }
}
