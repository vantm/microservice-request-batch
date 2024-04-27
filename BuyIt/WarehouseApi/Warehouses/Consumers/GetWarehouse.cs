using Contracts.Common;
using Contracts.Warehouses;
using Dapper;
using MassTransit;

namespace WarehouseApi.Warehouses.Consumers;

public class GetWarehouse(WarehouseDb db) : IConsumer<GetWarehouseRequest>
{
    public async Task Consume(ConsumeContext<GetWarehouseRequest> context)
    {
        var id = context.Message.Id;
        var warehouse = await FindAsync(id);

        if (warehouse == null)
        {
            if (context.IsResponseAccepted<NotFoundError>())
            {
                await context.RespondAsync<NotFoundError>(new { });
            }
        }
        else
        {
            if (context.IsResponseAccepted<GetWarehouseReply>())
            {
                await context.RespondAsync<GetWarehouseReply>(new
                {
                    Warehouse = warehouse
                });
            }
        }
    }

    public Task<Warehouse?> FindAsync(Guid id)
    {
        var conn = db.Open();

        const string SQL = """
        SELECT
            id,
            name,
            is_enabled AS IsEnabled,
            address_id AS AddressId,
            created_at AS CreatedAt,
            updated_at AS UpdatedAt
        FROM public.warehouse
        WHERE id = @id
        """;

        var warehouse = conn.QueryFirstOrDefaultAsync<Warehouse>(
            SQL, new { id, });

        return warehouse;
    }
}

public class GetWarehouseDefinition : ConsumerDefinition<GetWarehouse>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<GetWarehouse> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.DiscardFaultedMessages();
        endpointConfigurator.DiscardSkippedMessages();
    }
}
