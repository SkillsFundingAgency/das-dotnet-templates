using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SFA.DAS.Encoding;
using SFA.DAS.WebTemplateSourceName.Infrastructure.Configuration;

namespace SFA.DAS.WebTemplateSourceName.Web.StartupExtensions
{
    public static class AddConfigurationOptionsExtension
    {
        public static void AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<WebTemplateSourceNameWeb>(configuration.GetSection("WebTemplateSourceNameWeb"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<WebTemplateSourceNameWeb>>().Value);

            services.Configure<Infrastructure.Configuration.Authentication>(configuration.GetSection("Authentication"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<Infrastructure.Configuration.Authentication>>().Value);
            
            services.Configure<EncodingConfig>(configuration.GetSection("EncodingService"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<EncodingConfig>>().Value);

            services.Configure<CosmosDbConfiguration>(configuration.GetSection("CosmosDb"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<CosmosDbConfiguration>>().Value);
        }
    }
}
