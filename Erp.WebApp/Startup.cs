using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(Erp.WebApp.Startup))]

namespace Erp.WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure SignalR
            ConfigureSignalR(app);
        }
        
        private void ConfigureSignalR(IAppBuilder app)
        {
            // Configure SignalR
            var hubConfiguration = new HubConfiguration()
            {
                EnableDetailedErrors = true,
                EnableJavaScriptProxies = true
            };
            
            // Map SignalR hubs
            app.MapSignalR("/signalr", hubConfiguration);
        }
    }
}