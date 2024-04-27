using ApiDefaults;
using Carter;
using Contracts.Locations;
using LocationApi;
using MassTransit;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.AddAllApiDefaults();

builder.Services.AddSingleton<LocationDb>(implementationInstance:
    new(builder.Configuration.GetConnectionString("LocationDb")!));

builder.Services.AddCarter();

builder.Services
    .AddOptionsWithValidateOnStart<RabbitMqOptions>()
    .ValidateDataAnnotations()
    .BindConfiguration(RabbitMqOptions.Name);

builder.Services.AddMassTransit(c =>
{
    c.AddConsumersFromNamespaceContaining<LocationDb>();

    c.AddRequestClient<GetLocationAddressRequest>();
    c.AddRequestClient<AddLocationAddressRequest>();
    c.AddRequestClient<RemoveLocationAddressRequest>();

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

