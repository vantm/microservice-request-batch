using Contracts.Common;
using Contracts.Warehouses;
using MassTransit;

namespace WarehouseApi.Warehouses.Consumers;

public class RemoveWarehouse : IConsumer<RemoveWarehouseRequest>
{
    public Task Consume(ConsumeContext<RemoveWarehouseRequest> context)
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
