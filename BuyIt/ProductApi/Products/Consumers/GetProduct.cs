using Contracts.Common;
using Contracts.Products;
using Dapper;
using MassTransit;

namespace ProductApi.Products.Consumers;

public class GetProduct(ProductDb db) : IConsumer<GetProductRequest>
{
    public async Task Consume(ConsumeContext<GetProductRequest> context)
    {
        var product = await FindAsync(context.Message.Id);

        if (product == null)
        {
            if (context.IsResponseAccepted<NotFoundError>())
            {
                await context.RespondAsync<NotFoundError>(new { });
            }
        }
        else
        {
            if (context.IsResponseAccepted<GetProductReply>())
            {
                await context.RespondAsync<GetProductReply>(new
                {
                    Product = product
                });
            }
        }
    }

    private async Task<Product?> FindAsync(Guid id)
    {
        using var conn = db.Open();

        const string SQL = """
            SELECT
                id,
                name,
                price,
                created_at AS CreatedAt,
                updated_at AS UpdatedAt,
                is_enabled AS IsEnabled
            FROM public.product
            WHERE id = @id;
        """;

        var product = await conn.QueryFirstOrDefaultAsync<Product>(
            SQL,
            new { id });

        return product;
    }
}

public class GetProductDefinition : ConsumerDefinition<GetProduct>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<GetProduct> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.DiscardFaultedMessages();
        endpointConfigurator.DiscardSkippedMessages();
    }
}
