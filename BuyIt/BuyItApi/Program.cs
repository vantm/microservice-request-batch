using Elastic.Apm.NetCoreAll;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddYamlFile("yarp.yaml", optional: false, reloadOnChange: builder.Environment.IsDevelopment());

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("Yarp"))
    .ConfigureHttpClient((context, handler) =>
    {
        handler.ActivityHeadersPropagator = DistributedContextPropagator.CreateDefaultPropagator();
    });

builder.Services.AddCors(x =>
{
    x.AddPolicy("Development", p =>
    {
        p.AllowAnyOrigin();
        p.AllowAnyMethod();
        p.AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseAllElasticApm(app.Configuration);

if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");
}
else
{
    app.UseCors();
}

app.MapReverseProxy();

app.Run();
