using Contracts.Locations;
using Dapper;
using MassTransit;
using Microsoft.VisualBasic;

namespace LocationApi.Addresses.Consumers;

public class AddAddress(
    LocationDb db, TimeProvider timeProvider)
    : IConsumer<AddLocationAddressRequest>
{
    public async Task Consume(ConsumeContext<AddLocationAddressRequest> context)
    {
        var request = context.Message;
        var id = await InsertAddressAsync(context.Message);

        if (context.IsResponseAccepted<AddLocationAddressReply>())
        {
            await context.RespondAsync<AddLocationAddressReply>(new
            {
                Address = new
                {
                    Id = id,
                    request.AddressText,
                    request.District,
                    request.City
                }
            });
        }
    }

    private async Task<Guid> InsertAddressAsync(AddLocationAddressRequest request)
    {
        using var conn = db.Open();

        var id = Guid.NewGuid();

        await conn.ExecuteAsync(
            """
            INSERT INTO public.address(
                id, address_text, district, city, created_at, updated_at)
            VALUES (@Id, @AddressText, @District, @City, @CreatedAt, @UpdatedAt)
            """,
            new
            {
                Id = id,
                request.AddressText,
                request.District,
                request.City,
                CreatedAt = timeProvider.GetUtcNow(),
                UpdatedAt = timeProvider.GetUtcNow()
            });

        return id;
    }
}

