using System.Web.Http;
using Unity.WebApi;
using Unity;
using Erp.WebApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Configuration;
using System.Collections.Generic;
using Unity.Lifetime;

namespace Erp.WebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            
            // Configure Unity for Web API using the same container as MVC
            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.Container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
        
        private static IConfiguration BuildConfiguration()
        {
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            
            // Add configuration from Web.config appSettings
            var appSettings = new Dictionary<string, string>();
            foreach (string key in System.Configuration.ConfigurationManager.AppSettings.AllKeys)
            {
                appSettings[key] = System.Configuration.ConfigurationManager.AppSettings[key];
            }
            
            builder.AddInMemoryCollection(appSettings);
            return builder.Build();
        }
    }
}