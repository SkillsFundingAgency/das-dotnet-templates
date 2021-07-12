using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SFA.DAS.EmployerAccounts.Api.Client;
using SFA.DAS.WebTemplateSourceName.Infrastructure.Stubs;
using EmployerAccountsApiClient = SFA.DAS.WebTemplateSourceName.Infrastructure.Api.EmployerAccountsApiClient;

namespace SFA.DAS.WebTemplateSourceName.Web.StartupExtensions
{
    public static class AddEmployerAccountsApiExtensions
    {
        public static IServiceCollection AddEmployerAccountsApi(this IServiceCollection services, IConfiguration config, IWebHostEnvironment environment)
        {
            var useStub = config.GetValue<bool>("UseEmployerAccountApiStub");

            if (environment.IsDevelopment() && useStub)
            {
                services.AddSingleton<IEmployerAccountsApiClient, StubEmployerAccountsApiClient>();
            }
            else
            {
                services.AddSingleton<IEmployerAccountsApiClient, EmployerAccountsApiClient>();
            }

            return services;
        }
    }
}