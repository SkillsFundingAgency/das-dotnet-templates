using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SFA.DAS.WebTemplateSourceName.Data;

namespace SFA.DAS.WebTemplateSourceName.Api.StartupExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbConfiguration(this IServiceCollection services, string connectionString, IWebHostEnvironment hostingEnvironment)
        {
            services.AddTransient<DbConnection>(provider => new SqlConnection(connectionString));
            if (hostingEnvironment.IsDevelopment())
            {
                services.AddTransient<IDbContextFactory<WebTemplateSourceNameDbContext>>(provider => new DbContextFactory(new SqlConnection(connectionString), provider.GetService<ILoggerFactory>(), null));
            }
            else
            {
                services.AddTransient<IDbContextFactory<WebTemplateSourceNameDbContext>>(provider => new DbContextFactory(new SqlConnection(connectionString), provider.GetService<ILoggerFactory>(), new AzureServiceTokenProvider()));
            }
            services.AddTransient<WebTemplateSourceNameDbContext>(provider => provider.GetService<IDbContextFactory<WebTemplateSourceNameDbContext>>().CreateDbContext());
            services.AddTransient<IWebTemplateSourceNameDbContext>(provider => provider.GetService<WebTemplateSourceNameDbContext>());
        }
    }
}
