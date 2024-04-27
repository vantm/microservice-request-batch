using Carter;
using Contracts.Common;
using Contracts.Warehouses;
using Dapper;
using MassTransit;

namespace WarehouseApi.Warehouses;

public class WarehouseModule : CarterModule
{
    public WarehouseModule() : base("/warehouses")
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
                name,
                is_enabled AS IsEnabled,
                address_id AS AddressId,
                created_at AS CreatedAt,
                updated_at AS UpdatedAt
            FROM public.warehouse
            """;

            var warehouses = await conn.QueryAsync<Warehouse>(SQL);

            return Results.Ok(warehouses);
        });

        app.MapGet("{id:guid}", async (
            Guid id, IRequestClient<GetWarehouseRequest> client, CancellationToken ct) =>
        {
            var response = await client.GetResponse<GetWarehouseReply, NotFoundError>(
                new { Id = id }, ct);
            if (response.Is<NotFoundError>(out _))
            {
                return Results.NotFound();
            }
            if (response.Is<GetWarehouseReply>(out var reply))
            {
                return Results.Ok(reply.Message.Warehouse);
            }
            return Results.BadRequest("An unhandled error was happended");
        }).WithName("GetWarehouse");

        app.MapPost("", async (
            AddWarehouse request, IRequestClient<AddWarehouseRequest> client, CancellationToken ct) =>
        {
            var response = await client.GetResponse<AddWarehouseReply, ValidationError>(
                request, ct);

            if (response.Is<ValidationError>(out var validationErrorReply))
            {
                return Results.ValidationProblem(validationErrorReply.Message.Errors);
            }
            if (response.Is<AddWarehouseReply>(out var reply))
            {
                var product = reply.Message.Warehouse;
                return Results.CreatedAtRoute("GetWarehouse", new { id = product.Id }, product);
            }
            return Results.BadRequest("An unhandled error was happended");
        });
    }
}

record AddWarehouse : AddWarehouseRequest
{
    public string Name { get; set; } = "";
    public Guid AddressId { get; set; }
}
