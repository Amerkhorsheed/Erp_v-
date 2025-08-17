namespace Erp.WebApp.Services.Interfaces
{
    public interface IAuthService
    {
        bool IsAuthenticated { get; }
        string UserRole { get; }
        string Token { get; }

        void SignIn(string token, string userRole, string userName);
        void SignOut();
    }
}