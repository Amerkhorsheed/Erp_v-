using Erp.WebApp.Services;
using Erp.WebApp.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Erp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // These will be injected by your DI container.
        private readonly IApiService _apiService;
        private readonly IAuthService _authService;

        // Constructor for DI
        public HomeController(IApiService apiService, IAuthService authService)
        {
            _apiService = apiService;
            _authService = authService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var loginResponse = await _apiService.PostAsync<LoginViewModel, LoginResponseViewModel>("api/auth/login", model);

                if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
                {
                    ModelState.AddModelError("", "Login failed. Please check your credentials.");
                    return View(model);
                }

                _authService.SignIn(loginResponse.Token, loginResponse.Role);

                switch (loginResponse.Role)
                {
                    case "Admin":
                        return RedirectToAction("Index", "Admin");
                    case "Seller":
                        return RedirectToAction("Index", "Seller");
                    default:
                        ModelState.AddModelError("", "Your role is not authorized to access this system.");
                        return View(model);
                }
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while trying to log in.");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            _authService.SignOut();
            return RedirectToAction("Login", "Home");
        }
    }
}