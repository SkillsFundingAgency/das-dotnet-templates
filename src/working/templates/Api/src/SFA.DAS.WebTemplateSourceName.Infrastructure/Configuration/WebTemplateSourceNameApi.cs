namespace SFA.DAS.WebTemplateSourceName.Infrastructure.Configuration
{
    public class WebTemplateSourceNameApi
    {
        public string DatabaseConnectionString { get; set; }
        public string NServiceBusConnectionString { get; set; }
        public string NServiceBusLicense { get; set; }
        public string RedisConnectionString { get; set; }
        public string DataProtectionKeysDatabase { get; set; }
    }
}
