using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.WebTemplateSourceName.Infrastructure.Configuration;

namespace SFA.DAS.WebTemplateSourceName.Functions.StartupExtensions
{
    public static class CacheStartupExtensions
    {
        public static IServiceCollection AddCache(this IServiceCollection services, WebTemplateSourceNameFunctions config)
        {
            if (string.IsNullOrEmpty(config.RedisConnectionString))
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options => { options.Configuration = config.RedisConnectionString; });
            }

            return services;
        }
    }
}
