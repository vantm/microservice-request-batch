using ApiDefaults;
using Carter;
using Elastic.Apm.NetCoreAll;
using MassTransit;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
using WarehouseApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddAllApiDefaults();

builder.Services.AddSingleton<WarehouseDb>(implementationInstance:
    new(builder.Configuration.GetConnectionString("WarehouseDb")!));

builder.Services.AddCarter();

builder.Services
    .AddOptionsWithValidateOnStart<RabbitMqOptions>()
    .ValidateDataAnnotations()
    .BindConfiguration(RabbitMqOptions.Name);

builder.Services.AddMassTransit(c =>
{
    c.AddConsumersFromNamespaceContaining<WarehouseDb>();

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

