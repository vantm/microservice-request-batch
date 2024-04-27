using Carter;
using Contracts.Common;
using Contracts.Warehouses;
using Dapper;
using MassTransit;

namespace WarehouseApi.Batches;

public class BatchModule : CarterModule
{
    public BatchModule() : base("/batches")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("", async (WarehouseDb db) =>
        {
            using var conn = db.Open();

            string SQL = """
            SELECT
                id,
                key,
                product_id AS ProductId,
                warehouse_id AS WarehouseId,
                quantity,
                created_at AS CreatedAt,
                updated_at AS UpdatedAt
            FROM public.batch
            """;

            var batches = await conn.QueryAsync<Batch>(SQL);

            return Results.Ok(batches);
        });

        app.MapGet("{id:guid}", async (
            Guid id, IRequestClient<GetWarehouseBatchRequest> client, CancellationToken ct) =>
        {
            var response = await client.GetResponse<GetWarehouseBatchReply, NotFoundError>(
                new { Id = id }, ct);
            if (response.Is<NotFoundError>(out _))
            {
                return Results.NotFound();
            }
            if (response.Is<GetWarehouseBatchReply>(out var reply))
            {
                return Results.Ok(reply.Message.Batch);
            }
            return Results.BadRequest("An unhandled error was happended");
        }).WithName("GetBatch");

        app.MapPost("", async (
            AddBatch request, IRequestClient<AddWarehouseBatchRequest> client, CancellationToken ct) =>
        {
            var response = await client.GetResponse<AddWarehouseBatchReply, ValidationError>(
                request, ct);

            if (response.Is<ValidationError>(out var validationErrorReply))
            {
                return Results.ValidationProblem(validationErrorReply.Message.Errors);
            }
            if (response.Is<AddWarehouseBatchReply>(out var reply))
            {
                var product = reply.Message.Batch;
                return Results.CreatedAtRoute("GetBatch", new { id = product.Id }, product);
            }
            return Results.BadRequest("An unhandled error was happended");
        });
    }
}

record AddBatch : AddWarehouseBatchRequest
{
    public string Key { get; set; } = "";
    public Guid ProductId { get; set; }
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
}
