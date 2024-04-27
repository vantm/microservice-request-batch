using ApiDefaults;
using Carter;
using Contracts.Products;
using MassTransit;
using Microsoft.Extensions.Options;
using ProductApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddAllApiDefaults();

builder.Services.AddSingleton<ProductDb>(implementationInstance:
    new(builder.Configuration.GetConnectionString("ProductDb")!));

builder.Services.AddCarter();

builder.Services
    .AddOptionsWithValidateOnStart<RabbitMqOptions>()
    .ValidateDataAnnotations()
    .BindConfiguration(RabbitMqOptions.Name);

builder.Services.AddMassTransit(c =>
{
    c.AddConsumersFromNamespaceContaining<ProductDb>();

    c.AddRequestClient<GetProductRequest>();
    c.AddRequestClient<AddProductRequest>();
    c.AddRequestClient<RemoveProductRequest>();

    c.UsingRabbitMq((ctx, cfg) =>
    {
        var options = ctx.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

        cfg.Host(
            options.Host, options.Port, options.VirtualHost,
            h =>
            {
                h.Username(options.Username);
                h.Password(options.Password);
            });

        cfg.ConfigureEndpoints(ctx);
    });
});

var app = builder.Build();

app.UseAllApiDefaults();

app.MapCarter();

app.Run();
