using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;

// Make sure this namespace matches the namespace in your Program.cs file
namespace Erp_V1
{
    public class Startup
    {
        // This method is required by the OWIN self-hosting framework.
        // It configures your API server.
        public void Configuration(IAppBuilder app)
        {
            // Enable Cross-Origin Resource Sharing (CORS)
            // This allows your HTML pages to communicate with the API.
            app.UseCors(CorsOptions.AllowAll);

            // Create a new configuration for the Web API
            var config = new HttpConfiguration();

            // Enable attribute-based routing (e.g., [RoutePrefix], [Route])
            config.MapHttpAttributeRoutes();

            // Set up the default route for the API.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Tell the OWIN pipeline to use the Web API with our configuration.
            app.UseWebApi(config);
        }
    }
}