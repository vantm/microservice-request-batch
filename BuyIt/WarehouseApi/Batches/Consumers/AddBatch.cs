using Contracts.Warehouses;
using Dapper;
using MassTransit;

namespace WarehouseApi.Batches.Consumers;

public class AddBatch(WarehouseDb db, TimeProvider timeProvider)
    : IConsumer<AddWarehouseBatchRequest>
{
    public async Task Consume(ConsumeContext<AddWarehouseBatchRequest> context)
    {
        var request = context.Message;
        var id = await InsertAsync(request);

        if (context.IsResponseAccepted<AddWarehouseBatchReply>())
        {
            await context.RespondAsync<AddWarehouseBatchReply>(new
            {
                Batch = new
                {
                    Id = id,
                    request.Key,
                    request.ProductId,
                    request.WarehouseId,
                    request.Quantity,
                }
            });
        }
    }

    private async Task<Guid> InsertAsync(AddWarehouseBatchRequest request)
    {
        using var conn = db.Open();

        var id = Guid.NewGuid();

        await conn.ExecuteAsync(
            """
            INSERT INTO public.batch(
                id, key, product_id, warehouse_id, quantity, created_at, updated_at)
            VALUES (@Id, @Key, @ProductId, @WarehouseId, @Quantity, @CreatedAt, @UpdatedAt);
            """,
            new
            {
                Id = id,
                request.Key,
                request.ProductId,
                request.WarehouseId,
                request.Quantity,
                CreatedAt = timeProvider.GetUtcNow(),
                UpdatedAt = timeProvider.GetUtcNow()
            });

        return id;
    }
}
