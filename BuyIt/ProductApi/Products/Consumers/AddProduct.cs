using Contracts.Products;
using Dapper;
using MassTransit;

namespace ProductApi.Products.Consumers;

public class AddProduct(ProductDb db, TimeProvider timeProvider)
    : IConsumer<AddProductRequest>
{
    public async Task Consume(ConsumeContext<AddProductRequest> context)
    {
        var request = context.Message;
        var id = await InsertProductAsync(request);

        if (context.IsResponseAccepted<AddProductReply>())
        {
            await context.RespondAsync<AddProductReply>(new
            {
                Product = new
                {
                    Id = id,
                    request.Name,
                    request.Price,
                    IsEnabled = false
                }
            });
        }
    }

    private async Task<Guid> InsertProductAsync(AddProductRequest request)
    {
        using var conn = db.Open();

        var id = Guid.NewGuid();

        await conn.ExecuteAsync(
            """
            INSERT INTO public.product(
                id, name, price, is_enabled, created_at, updated_at)
            VALUES (@Id, @Name, @Price, @IsEnabled, @CreatedAt, @UpdatedAt);
            """,
            new
            {
                Id = id,
                request.Name,
                request.Price,
                IsEnabled = false,
                CreatedAt = timeProvider.GetUtcNow(),
                UpdatedAt = timeProvider.GetUtcNow()
            });

        return id;
    }
}
