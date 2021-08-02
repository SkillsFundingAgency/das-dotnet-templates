using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Authorization.EmployerUserRoles.Options;
using SFA.DAS.Authorization.Mvc.Attributes;
using SFA.DAS.WebTemplateSourceName.Web.Authentication;
using SFA.DAS.WebTemplateSourceName.Web.Models.Secure;

namespace SFA.DAS.WebTemplateSourceName.Web.Controllers
{
    [Route("secure/{encodedAccountId}")]
    [DasAuthorize(EmployerUserRole.OwnerOrTransactor)]
    public class SecureController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public SecureController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Index(string encodedAccountId)
        {
            var viewmodel = new IndexViewModel
            {
                UserId = _authenticationService.UserId,
                DisplayName = _authenticationService.UserDisplayName
            };

            ViewBag.HideNav = false;

            return View(viewmodel);
        }
    }
}