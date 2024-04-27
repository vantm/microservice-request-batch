using Carter;
using Contracts.Common;
using Contracts.Products;
using Dapper;
using MassTransit;

namespace ProductApi.Products;

public class ProductModule : CarterModule
{
    public ProductModule() : base("/products")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("", async (ProductDb db) =>
        {
            using var conn = db.Open();

            string SQL = """
            SELECT
                id,
                name,
                is_enabled AS IsEnabled,
                price,
                created_at AS CreatedAt,
                updated_at AS UpdatedAt
            FROM public.product
            """;

            var products = await conn.QueryAsync<Product>(SQL);

            return Results.Ok(products);
        });

        app.MapGet("{id:guid}", async (
            Guid id, IRequestClient<GetProductRequest> client, CancellationToken ct) =>
        {
            var response = await client.GetResponse<GetProductReply, NotFoundError>(
                new { Id = id }, ct);
            if (response.Is<NotFoundError>(out _))
            {
                return Results.NotFound();
            }
            if (response.Is<GetProductReply>(out var reply))
            {
                return Results.Ok(reply.Message.Product);
            }
            return Results.BadRequest("An unhandled error was happended");
        }).WithName("GetProduct");

        app.MapPost("", async (
            AddProduct request, IRequestClient<AddProductRequest> client, CancellationToken ct) =>
        {
            var response = await client.GetResponse<AddProductReply, ValidationError>(
                request, ct);

            if (response.Is<ValidationError>(out var validationErrorReply))
            {
                return Results.ValidationProblem(validationErrorReply.Message.Errors);
            }
            if (response.Is<AddProductReply>(out var reply))
            {
                var product = reply.Message.Product;
                return Results.CreatedAtRoute("GetProduct", new { id = product.Id }, product);
            }
            return Results.BadRequest("An unhandled error was happended");
        });
    }
}

record AddProduct : AddProductRequest
{
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}
