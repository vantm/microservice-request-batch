using Contracts.Common;
using Contracts.Products;
using MassTransit;

namespace ProductApi.Products.Consumers;

public class RemoveProduct(ProductDb db) : IConsumer<RemoveProductRequest>
{
    public Task Consume(ConsumeContext<RemoveProductRequest> context)
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
