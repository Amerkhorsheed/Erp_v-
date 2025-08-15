using Erp.WebApp.Services;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Erp.WebApp.Controllers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly IAuthService _authService;

        public CustomAuthorizeAttribute()
        {
            // In a real DI setup, this would be injected.
            // For simplicity here, we create a new instance.
            _authService = new AuthService();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Allow access to Home/Login without authentication
            var routeData = httpContext.Request.RequestContext.RouteData;
            var controller = routeData.Values["controller"]?.ToString();
            var action = routeData.Values["action"]?.ToString();
            
            if (string.Equals(controller, "Home", System.StringComparison.OrdinalIgnoreCase) && 
                string.Equals(action, "Login", System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (!_authService.IsAuthenticated)
            {
                return false;
            }

            if (string.IsNullOrEmpty(Roles))
            {
                return true; // Any authenticated user is allowed.
            }

            var allowedRoles = Roles.Split(',').Select(r => r.Trim());
            return allowedRoles.Contains(_authService.UserRole);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Login" }
                });
        }
    }
}