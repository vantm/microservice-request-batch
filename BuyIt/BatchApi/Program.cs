using ApiDefaults;
using BatchApi;
using BatchApi.Core;
using BatchApi.Core.Impl;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.AddAllApiDefaults();

builder.Services.AddCarter();

builder.Services.AddScoped<IBatchProcessor, DefaultBatchProcessor>();
builder.Services.AddScoped<IBatchExecutorFactory, DefaultBatchExecutorFactory>();

builder.Services
    .AddOptionsWithValidateOnStart<BatchOptions>()
    .BindConfiguration(BatchOptions.Name);

var options = new BatchOptions();
builder.Configuration.Bind(BatchOptions.Name, options);

foreach (var service in options.Endpoints)
{
    builder.Services.AddHttpClient(service.ServicePath, client =>
    {
        client.BaseAddress = new(service.Url);
    });
}

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseAllApiDefaults();

app.MapCarter();

app.Run();
