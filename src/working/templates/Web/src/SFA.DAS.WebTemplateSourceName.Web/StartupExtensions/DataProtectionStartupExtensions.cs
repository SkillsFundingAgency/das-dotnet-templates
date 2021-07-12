using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SFA.DAS.WebTemplateSourceName.Infrastructure.Configuration;
using StackExchange.Redis;

namespace SFA.DAS.WebTemplateSourceName.Web.StartupExtensions
{
    public static class DataProtectionStartupExtensions
    {
        public static IServiceCollection AddDasDataProtection(this IServiceCollection services, WebTemplateSourceNameWeb webConfiguration, IWebHostEnvironment environment)
        {
            if (!environment.IsDevelopment())
            {
                var redisConnectionString = webConfiguration.RedisConnectionString;
                var dataProtectionKeysDatabase = webConfiguration.DataProtectionKeysDatabase;

                var redis = ConnectionMultiplexer.Connect($"{redisConnectionString},{dataProtectionKeysDatabase}");

                services.AddDataProtection()
                    .SetApplicationName("das-WebTemplateSourceName-web")
                    .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys");
            }
            return services;
        }
    }
}
