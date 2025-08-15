using Erp.WebApp.Services;
using PdfSharp.Fonts;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Host.SystemWeb;
using Owin;

//[assembly: OwinStartup(typeof(Erp.WebApp.Startup))]

namespace Erp.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // This line configures your Web API controllers (if any). It should stay.
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // These lines configure standard MVC features. They should stay.
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalFontSettings.FontResolver = new FontResolver();

            // Configure SignalR
            GlobalHost.DependencyResolver.Register(typeof(Microsoft.AspNet.SignalR.Hubs.IHubActivator), () => new Microsoft.AspNet.SignalR.Hubs.DefaultHubActivator(GlobalHost.DependencyResolver));



            // ==========================================================
            // ==  FIX: DELETE OR COMMENT OUT THIS LINE.               ==
            // ==  UnityMvcActivator.cs now handles this automatically. ==
            // ==========================================================
            // UnityConfig.RegisterComponents(); 
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                // Log the exception details
                System.Diagnostics.Debug.WriteLine($"Application Error: {exception.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {exception.StackTrace}");
                
                if (exception.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner Exception: {exception.InnerException.Message}");
                    System.Diagnostics.Debug.WriteLine($"Inner Stack Trace: {exception.InnerException.StackTrace}");
                }
                
                // Also write to console for immediate visibility
                Console.WriteLine($"[ERROR] {DateTime.Now}: {exception.Message}");
                if (exception.InnerException != null)
                {
                    Console.WriteLine($"[INNER ERROR] {exception.InnerException.Message}");
                }
            }
        }
    }
}