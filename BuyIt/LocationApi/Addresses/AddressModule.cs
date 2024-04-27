using Carter;
using Contracts.Common;
using Contracts.Locations;
using Dapper;
using MassTransit;

namespace LocationApi.Addresses;

public class AddressModule : CarterModule
{
    public AddressModule() : base("/addresses")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("", async (LocationDb db) =>
        {
            using var conn = db.Open();

            string SQL = """
            SELECT
                id,
                address_text AS AddressText,
                district,
                city,
                created_at AS CreatedAt,
                updated_at AS UpdatedAt
            FROM public.address
            """;

            var addresses = await conn.QueryAsync<Address>(SQL);

            return Results.Ok(addresses);
        });

        app.MapGet("{id:guid}", async (
            Guid id, IRequestClient<GetLocationAddressRequest> client,
            CancellationToken ct) =>
        {
            var response = await client.GetResponse<GetLocationAddressReply, NotFoundError>(
                new { Id = id }, ct);

            if (response.Is<GetLocationAddressReply>(out var reply))
            {
                return Results.Ok(reply.Message.Address);
            }

            if (response.Is<NotFoundError>(out _))
            {
                return Results.NotFound();
            }

            return Results.BadRequest("An unhandled error was happended");
        }).WithName("GetLocationAddress");

        app.MapPost("", async (
            AddRequest req,
            IRequestClient<AddLocationAddressRequest> client,
            CancellationToken ct) =>
        {
            var response = await client.GetResponse<AddLocationAddressReply, ValidationResult>(
                req, ct);

            if (response.Is<AddLocationAddressReply>(out var reply))
            {
                return Results.CreatedAtRoute(
                    "GetLocationAddress",
                    new { reply.Message.Address.Id },
                    reply.Message.Address);
            }

            if (response.Is<ValidationError>(out var validationErrorReply))
            {
                return Results.ValidationProblem(validationErrorReply.Message.Errors);
            }

            return Results.BadRequest("An unhandled error was happended");
        });

        app.MapDelete("{id:guid}", async (
            Guid id, IRequestClient<RemoveLocationAddressRequest> client,
            CancellationToken ct) =>
        {
            var response = await client.GetResponse<RemoveLocationAddressReply, ValidationError, NotFoundError>(
                new { Id = id }, ct);

            if (response.Is<RemoveLocationAddressReply>(out _))
            {
                return Results.NoContent();
            }
            if (response.Is<NotFoundError>(out _))
            {
                return Results.NotFound();
            }
            if (response.Is<ValidationError>(out var validationErrorReply))
            {
                return Results.ValidationProblem(validationErrorReply.Message.Errors);
            }
            return Results.BadRequest("An unhandled error was happended");
        });
    }
}

record AddRequest : AddLocationAddressRequest
{
    public string AddressText { get; set; } = "";
    public string City { get; set; } = "";
    public string District { get; set; } = "";
}
