using System.Text.Json;
using EchoServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(c =>
{
    c.AddDefaultPolicy(p =>
    {
        p.AllowAnyOrigin();
        p.AllowAnyHeader();
        p.AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();

var jsonSerializerOptions = new JsonSerializerOptions()
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

app.MapPost("/{**path}", async (string path, HttpRequest req) =>
{
    using var reader = new StreamReader(req.Body);

    var json = await reader.ReadToEndAsync();
    var request = JsonSerializer.Deserialize<BatchRequest>(
        json, jsonSerializerOptions);

    var data = request?.Requests
        .Select(x => new BatchResponseEntry(true, x.RequestId, new
        {
            Id = Guid.NewGuid()
        }))
        .ToArray();

    return Results.Ok(new
    {
        path,
        data
    });
});

app.Run();
