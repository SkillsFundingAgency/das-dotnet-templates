using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SFA.DAS.WebTemplateSourceName.Infrastructure.Configuration;

namespace SFA.DAS.WebTemplateSourceName.Api.StartupExtensions
{
    public static class AddConfigurationOptionsExtension
    {
        public static void AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<WebTemplateSourceNameApi>(configuration.GetSection("WebTemplateSourceNameApi"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<WebTemplateSourceNameApi>>().Value);
        }
    }
}
