using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.WebTemplateSourceName.Infrastructure.Configuration;
using StackExchange.Redis;

namespace SFA.DAS.WebTemplateSourceName.Functions.StartupExtensions
{
    public static class DataProtectionStartupExtensions
    {
        public static IServiceCollection AddDasDataProtection(this IServiceCollection services, WebTemplateSourceNameFunctions configuration)
        {
            if (string.IsNullOrEmpty(configuration.RedisConnectionString) ||
                string.IsNullOrEmpty(configuration.DataProtectionKeysDatabase))
            {
                return services;
            }
            
            services.AddDataProtection()
                .SetApplicationName("das-WebTemplateSourceName-functions")
                .PersistKeysToStackExchangeRedis(CreateConnectionMultiplexer(configuration), "DataProtection-Keys");

            return services;
        }

        private static ConnectionMultiplexer CreateConnectionMultiplexer(WebTemplateSourceNameFunctions configuration) => ConnectionMultiplexer.Connect($"{configuration.RedisConnectionString},{configuration.DataProtectionKeysDatabase}");
    }
}
