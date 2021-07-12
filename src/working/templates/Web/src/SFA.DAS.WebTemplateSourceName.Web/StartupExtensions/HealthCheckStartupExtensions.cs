using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.WebTemplateSourceName.Infrastructure.Configuration;
using SFA.DAS.WebTemplateSourceName.Web.Extensions;

namespace SFA.DAS.WebTemplateSourceName.Web.StartupExtensions
{
    public static class HealthCheckStartupExtensions
    {
        public static IServiceCollection AddDasHealthChecks(this IServiceCollection services, WebTemplateSourceNameWeb config)
        {
            services
                .AddHealthChecks()
                .AddRedis(config.RedisConnectionString, "Redis health check")
            ;

            return services;
        }

        public static IApplicationBuilder UseDasHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/ping", new HealthCheckOptions
            {
                Predicate = (_) => false,
                ResponseWriter = (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync("");
                }
            });

            return app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = (c, r) => c.Response.WriteJsonAsync(new
                {
                    r.Status,
                    r.TotalDuration,
                    Results = r.Entries.ToDictionary(
                        e => e.Key,
                        e => new
                        {
                            e.Value.Status,
                            e.Value.Duration,
                            e.Value.Description,
                            e.Value.Data
                        })
                })
            });
        }
    }
}
