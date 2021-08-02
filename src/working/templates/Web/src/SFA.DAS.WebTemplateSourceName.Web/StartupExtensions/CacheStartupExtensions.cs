﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SFA.DAS.WebTemplateSourceName.Infrastructure.Configuration;

namespace SFA.DAS.WebTemplateSourceName.Web.StartupExtensions
{
    public static class CacheStartupExtensions
    {
        public static IServiceCollection AddCache(this IServiceCollection services, IHostEnvironment environment, WebTemplateSourceNameWeb config)
        {
            if (environment.IsDevelopment())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(
                    options => { options.Configuration = config.RedisConnectionString; });
            }

            return services;
        }
    }
}
