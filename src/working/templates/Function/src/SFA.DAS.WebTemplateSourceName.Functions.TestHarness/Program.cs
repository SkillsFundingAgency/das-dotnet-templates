using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus.Primitives;
using Microsoft.Extensions.Configuration;
using NServiceBus;
using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.WebTemplateSourceName.Messages.Events;
using SFA.DAS.NServiceBus.Configuration;
using SFA.DAS.NServiceBus.Configuration.NewtonsoftJsonSerializer;

namespace SFA.DAS.WebTemplateSourceName.Functions.TestHarness
{
    internal class Program
    {
        private const string EndpointName = "SFA.DAS.WebTemplateSourceName.MessageHandlers";
        private const string ConfigName = "SFA.DAS.WebTemplateSourceName.Functions";

        public static async Task Main()
        {
            var builder = new ConfigurationBuilder()
                .AddAzureTableStorage(ConfigName);

            var configuration = builder.Build();

            var endpointConfiguration = new EndpointConfiguration(EndpointName)
                .UseErrorQueue($"{EndpointName}-errors")
                .UseInstallers()
                .UseMessageConventions()
                .UseNewtonsoftJsonSerializer();

            var connString = configuration[$"{ConfigName}:WebTemplateSourceNameFunctions:NServiceBusConnectionString"];

            if (connString == "UseDevelopmentStorage=true")
            {
                var t = endpointConfiguration.UseTransport<LearningTransport>();
                    t.StorageDirectory(
                    Path.Combine(
                        Directory.GetCurrentDirectory()
                            .Substring(0, Directory.GetCurrentDirectory().IndexOf("src")),
                        @"src\.learningtransport"));
                    t.Routing().RouteToEndpoint(typeof(CreatedAccountEvent), "SFA.DAS.WebTemplateSourceName.CreatedAccount");
            }

            else
            {
                var t = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
                t.ConnectionString(connString)
                    .CustomTokenProvider(TokenProvider.CreateManagedIdentityTokenProvider());
                t.Routing().RouteToEndpoint(typeof(CreatedAccountEvent), "SFA.DAS.WebTemplateSourceName.CreatedAccount");
            }

            var endpoint = await Endpoint.Start(endpointConfiguration);
            var testHarness = new TestHarness(endpoint);

            await testHarness.Run();
            await endpoint.Stop();
        }
    }
}
