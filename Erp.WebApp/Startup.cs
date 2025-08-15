using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Erp.WebApp.Startup))]

namespace Erp.WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Basic OWIN configuration
            // SignalR and notification functionality has been removed
        }
    }
}