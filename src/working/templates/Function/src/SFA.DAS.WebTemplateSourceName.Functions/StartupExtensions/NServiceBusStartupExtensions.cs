using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NServiceBus;
using SFA.DAS.WebTemplateSourceName.Infrastructure.Configuration;

namespace SFA.DAS.WebTemplateSourceName.Functions.StartupExtensions
{
    public static class NServiceBusStartupExtensions
    {
        public static IServiceCollection AddNServiceBus(this IServiceCollection services, WebTemplateSourceNameFunctions config, ILogger logger)
        {
            if (config.NServiceBusConnectionString.Equals("UseDevelopmentStorage=true", StringComparison.CurrentCultureIgnoreCase))
            {
                services.AddNServiceBus(logger, (options) =>
                {
                    options.EndpointConfiguration = (endpoint) =>
                    {
                        endpoint.UseTransport<LearningTransport>().StorageDirectory(
                            Path.Combine(
                                Directory.GetCurrentDirectory()
                                    .Substring(0, Directory.GetCurrentDirectory().IndexOf("src")),
                                @"src\.learningtransport"));
                        
                        return endpoint;
                    };
                });
            }
            else
            {
                Environment.SetEnvironmentVariable("NServiceBusConnectionString", config.NServiceBusConnectionString);
                services.AddNServiceBus(logger);
            }

            return services;
        }
    }
}
