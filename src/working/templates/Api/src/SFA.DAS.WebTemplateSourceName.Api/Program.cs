using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SFA.DAS.NServiceBus.Configuration.MicrosoftDependencyInjection;
using NLog.Web;

namespace SFA.DAS.WebTemplateSourceName.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)                
                .UseNServiceBusContainer()
                .UseStartup<Startup>()
                .UseNLog();
                
    }
}
