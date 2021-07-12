using NServiceBus;
using SFA.DAS.WebTemplateSourceName.Messages.Commands;

namespace SFA.DAS.WebTemplateSourceName.Extensions
{
    public static class RoutingSettingsExtensions
    {
        private const string WebTemplateSourceNameMessageHandler = "SFA.DAS.WebTemplateSourceName.MessageHandlers";


        public static void AddRouting(this RoutingSettings routingSettings)
        {
            routingSettings.RouteToEndpoint(typeof(RunHealthCheckCommand), WebTemplateSourceNameMessageHandler);
        }
    }
}
