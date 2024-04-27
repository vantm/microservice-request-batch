using Contracts.Common;
using Contracts.Warehouses;
using Dapper;
using MassTransit;

namespace WarehouseApi.Batches.Consumers;

public class GetBatch(WarehouseDb db) : IConsumer<GetWarehouseBatchRequest>
{
    public async Task Consume(ConsumeContext<GetWarehouseBatchRequest> context)
    {
        var id = context.Message.Id;
        var batch = await FindAsync(id);

        if (batch == null)
        {
            if (context.IsResponseAccepted<NotFoundError>())
            {
                await context.RespondAsync<NotFoundError>(new { });
            }
        }
        else
        {
            if (context.IsResponseAccepted<GetWarehouseBatchReply>())
            {
                await context.RespondAsync<GetWarehouseBatchReply>(new
                {
                    Batch = batch
                });
            }
        }
    }

    private async Task<Batch?> FindAsync(Guid id)
    {
        var conn = db.Open();

        const string SQL = """
        SELECT
            "id",
            "key",
            product_id AS ProductId,
            warehouse_id AS WarehouseId,
            quantity,
            created_at AS CreatedAt,
            updated_at AS UpdatedAt
        FROM public.batch
        WHERE id = @id
        """;

        var batch = await conn.QueryFirstOrDefaultAsync<Batch>(
            SQL, new { id });

        return batch;
    }
}

public class GetBatchDefinition :ConsumerDefinition<GetBatch>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<GetBatch> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.DiscardFaultedMessages();
        endpointConfigurator.DiscardSkippedMessages();
    }
}
