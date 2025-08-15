using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Seller.API.Filters; // Your global exception filter
using System.Text;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Unity.WebApi;

[assembly: OwinStartup(typeof(Seller.API.Startup))]

namespace Seller.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // 1. Configure Dependency Injection
            var container = UnityConfig.RegisterComponents();
            config.DependencyResolver = new UnityDependencyResolver(container);

            // 2. Register Global API Filters
            config.Filters.Add(new ApiExceptionFilterAttribute());

            // 3. Configure JSON Formatting to use camelCase
            // This ensures .NET properties like "SaleIds" become "saleIds" in the JSON output.
            var jsonSettings = config.Formatters.JsonFormatter.SerializerSettings;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // 4. Enable Cross-Origin Resource Sharing (CORS)
            app.UseCors(CorsOptions.AllowAll);

            // 5. Configure Authentication
            ConfigureAuth(app);

            // 6. Map API Routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // 7. Use Web API Middleware
            app.UseWebApi(config);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyThatIsLongAndSecure"));

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "YourAPI",
                        ValidAudience = "YourApp",
                        IssuerSigningKey = securityKey
                    }
                });
        }
    }
}