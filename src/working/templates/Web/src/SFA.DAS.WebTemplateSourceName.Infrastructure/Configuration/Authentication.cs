namespace SFA.DAS.WebTemplateSourceName.Infrastructure.Configuration
{
    public class Authentication
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string BaseAddress { get; set; }
        public bool UsePkce { get; set; }
        public string Scopes { get; set; }
    }
}
