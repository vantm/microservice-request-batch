using Contracts.Common;
using Contracts.Warehouses;
using MassTransit;

namespace WarehouseApi.Batches.Consumers;

public class RemoveBatch(WarehouseDb db) : IConsumer<RemoveWarehouseBatchRequest>
{
    public Task Consume(ConsumeContext<RemoveWarehouseBatchRequest> context)
    {
        if (context.IsResponseAccepted<ValidationError>())
        {
            var errors = new Dictionary<string, string[]>()
            {
                [nameof(context.Message.Id)] = ["Not supported yet"]
            };

            return context.RespondAsync<ValidationError>(new
            {
                Errors = errors
            });
        }

        return Task.CompletedTask;
    }
}
