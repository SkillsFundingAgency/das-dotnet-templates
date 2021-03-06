using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SFA.DAS.Employer.Shared.UI;
using SFA.DAS.Employer.Shared.UI.Configuration;
using SFA.DAS.EmployerUrlHelper.Configuration;
using SFA.DAS.EmployerUrlHelper.DependencyResolution;

namespace SFA.DAS.WebTemplateSourceName.Web.StartupExtensions
{
    public static class AddEmployerSharedUIExtensions
    {
        public static void AddEmployerSharedUI(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationConfig = configuration.GetSection<Infrastructure.Configuration.Authentication>();
            services.AddEmployerUrlHelper(configuration);
            services.AddMaMenuConfiguration(configuration, "signout", authenticationConfig.ClientId);

            services.Configure<EmployerUrlHelperConfiguration>(configuration.GetSection("EmployerUrlHelper"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<EmployerUrlHelperConfiguration>>().Value);

            services.Configure<MaPageConfiguration>(configuration.GetSection("MaPageConfiguration"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<MaPageConfiguration>>().Value);
            services.PostConfigure<MaPageConfiguration>((options => options.LocalLogoutRouteName = "signout"));
        }
    }
}
