using System.Web;

namespace Erp.WebApp.Services
{
    public interface IAuthService
    {
        bool IsAuthenticated { get; }
        string UserRole { get; }
        string Token { get; }
        void SignIn(string token, string role);
        void SignOut();
    }

    public class AuthService : IAuthService
    {
        public bool IsAuthenticated => HttpContext.Current.Session["JwtToken"] != null;
        public string UserRole => HttpContext.Current.Session["UserRole"] as string;
        public string Token => HttpContext.Current.Session["JwtToken"] as string;

        public void SignIn(string token, string role)
        {
            HttpContext.Current.Session["JwtToken"] = token;
            HttpContext.Current.Session["UserRole"] = role;
        }

        public void SignOut()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }
    }
}