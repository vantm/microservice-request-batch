using Contracts.Common;
using Contracts.Locations;
using Dapper;
using MassTransit;

namespace LocationApi.Addresses.Consumers;

public class GetAddress(LocationDb db)
    : IConsumer<GetLocationAddressRequest>
{
    public async Task Consume(ConsumeContext<GetLocationAddressRequest> context)
    {
        var address = await FindAsync(context.Message.Id);

        if (address == null)
        {
            if (context.IsResponseAccepted<NotFoundError>())
            {
                await context.RespondAsync<NotFoundError>(new { });
            }
        }
        else
        {
            if (context.IsResponseAccepted<GetLocationAddressReply>())
            {
                await context.RespondAsync<GetLocationAddressReply>(new
                {
                    Address = address
                });
            }
        }
    }

    private async Task<Address?> FindAsync(Guid id)
    {
        using var conn = db.Open();

        const string SQL = """
            SELECT id, address_text AS AddressText, district, city
            FROM public.address
            WHERE id = @id
            """;

        return await conn.QueryFirstOrDefaultAsync<Address>(
            SQL, new { id });
    }
}

public class GetAddressDefinition : ConsumerDefinition<GetAddress>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<GetAddress> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.DiscardFaultedMessages();
        endpointConfigurator.DiscardSkippedMessages();
    }
}

