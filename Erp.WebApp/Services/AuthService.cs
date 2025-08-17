using System.Web;
using Erp.WebApp.Services.Interfaces;

namespace Erp.WebApp.Services
{
    public class AuthService : IAuthService
    {
        public bool IsAuthenticated => HttpContext.Current.Session["JwtToken"] != null;
        public string UserRole => HttpContext.Current.Session["UserRole"] as string;
        public string Token => HttpContext.Current.Session["JwtToken"] as string;

        public void SignIn(string token, string userRole, string userName)
        {
            HttpContext.Current.Session["JwtToken"] = token;
            HttpContext.Current.Session["UserRole"] = userRole;
            HttpContext.Current.Session["UserName"] = userName;
        }

        public void SignOut()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }
    }
}