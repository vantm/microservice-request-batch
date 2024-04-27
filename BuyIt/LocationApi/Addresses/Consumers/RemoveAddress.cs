using Contracts.Common;
using Contracts.Locations;
using MassTransit;

namespace LocationApi.Addresses.Consumers;

public class RemoveAddress(LocationDb db)
    : IConsumer<RemoveLocationAddressRequest>
{
    public Task Consume(ConsumeContext<RemoveLocationAddressRequest> context)
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
