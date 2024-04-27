using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Builder;

namespace ApiDefaults;

public static class WebApplicationExtensions
{
    public static void UseAllApiDefaults(this WebApplication app)
    {
        app.UseForwardedHeaders();

        app.UseHealthChecks("/hc");

        app.UseAllElasticApm(app.Configuration);
    }
}

