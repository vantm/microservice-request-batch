using MassTransit;
using Contracts.Warehouses;
using Dapper;

namespace WarehouseApi.Warehouses.Consumers;

public class AddWarehouse(WarehouseDb db, TimeProvider timeProvider)
    : IConsumer<AddWarehouseRequest>
{
    public async Task Consume(ConsumeContext<AddWarehouseRequest> context)
    {
        var request = context.Message;
        var id = await InsertAsync(request);

        if (context.IsResponseAccepted<AddWarehouseReply>())
        {
            await context.RespondAsync<AddWarehouseReply>(new
            {
                Warehouse = new
                {
                    Id = id,
                    request.Name,
                    request.AddressId,
                    IsEnabled = false,
                }
            });
        }
    }

    private async Task<Guid> InsertAsync(AddWarehouseRequest request)
    {
        using var conn = db.Open();

        var id = Guid.NewGuid();

        await conn.ExecuteAsync(
            """
            INSERT INTO public.warehouse(
                id, name, is_enabled, address_id, created_at, updated_at)
            VALUES (@Id, @Name, @IsEnabled, @AddressId, @CreatedAt, @UpdatedAt);
            """,
            new
            {
                Id = id,
                request.Name,
                request.AddressId,
                IsEnabled = false,
                CreatedAt = timeProvider.GetUtcNow(),
                UpdatedAt = timeProvider.GetUtcNow()
            });

        return id;
    }
}
