using System;
using System.IO;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus.ObjectBuilder.MSDependencyInjection;
using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.WebTemplateSourceName.Api.StartupExtensions;
using SFA.DAS.WebTemplateSourceName.Data;
using SFA.DAS.WebTemplateSourceName.Infrastructure.Configuration;
using SFA.DAS.UnitOfWork.NServiceBus.Features.ClientOutbox.DependencyResolution.Microsoft;

namespace SFA.DAS.WebTemplateSourceName.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;

            var config = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile("appsettings.Development.json", true)
#endif
                .AddEnvironmentVariables();

            if (!configuration["Environment"].Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
            {
                config.AddAzureTableStorage(options =>
                    {
                        options.ConfigurationKeys = configuration["ConfigNames"].Split(",");
                        options.StorageConnectionString = configuration["ConfigurationStorageConnectionString"];
                        options.EnvironmentName = configuration["Environment"];
                        options.PreFixConfigurationKeys = false;
                    }
                );
            }

            Configuration = config.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddNLog();
            services.AddConfigurationOptions(Configuration);
            var config = Configuration.GetSection<WebTemplateSourceNameApi>();

            services.AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                    fv.RegisterValidatorsFromAssemblyContaining<DbContextFactory>();
                });

            services.AddMediatR(typeof(DbContextFactory).Assembly);
            services.AddDasHealthChecks(config);
            services.AddDbConfiguration(config.DatabaseConnectionString, _environment);
            services.AddNServiceBusClientUnitOfWork();
            services.AddCache(config, _environment)
                    .AddDasDataProtection(config, _environment)
                    .AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseDasHealthChecks();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.AddSwagger();
        }

        public void ConfigureContainer(UpdateableServiceProvider serviceProvider)
        {
            var config = Configuration.GetSection<WebTemplateSourceNameApi>();
            serviceProvider.StartNServiceBus(config, _environment);
        }
    }
}
