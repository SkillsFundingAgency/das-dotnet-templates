using SFA.DAS.Http.Configuration;

namespace SFA.DAS.WebTemplateSourceName.Infrastructure.Configuration
{
    public class WebTemplateSourceNameApiConfiguration : IApimClientConfiguration
    {
        public string ApiBaseUrl { get; set; }
        public string SubscriptionKey { get; set; }
        public string ApiVersion { get; set; }
    }
}
