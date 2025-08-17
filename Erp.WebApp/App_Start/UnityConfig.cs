using Erp.WebApp.Services;
using Erp.WebApp.Services.Interfaces;
using Erp_V1.DAL.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;

namespace Erp.WebApp
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        public static void RegisterTypes(IUnityContainer container)
        {
            // --- Configuration Registrations ---
            //container.RegisterInstance<IConfiguration>(BuildConfiguration(), new ContainerControlledLifetimeManager());
            //container.RegisterType(typeof(ILogger<>), typeof(SimpleLogger<>));

            // --- Database Context Registration ---
            // PerRequestLifetimeManager ensures one DbContext instance per web request.
            container.RegisterType<erp_v2Entities>(new PerRequestLifetimeManager());

            // --- Service Registrations ---
            container.RegisterType<IAuthService, AuthService>();
            container.RegisterType<Erp.WebApp.Services.Interfaces.IEmailService, Erp.WebApp.Services.EmailService>();
            container.RegisterType<ISalesManagementEmailService, SalesManagementEmailService>();
            container.RegisterType<Erp.WebApp.Services.Interfaces.IInvoicePdfService, Erp.WebApp.Services.InvoicePdfService>();
            container.RegisterType<Erp.WebApp.Services.Interfaces.INotificationService, Erp.WebApp.Services.NotificationService>();

            // --- BLL Registrations ---
            container.RegisterType<Erp_V1.BLL.InvoiceBLL>();
            container.RegisterType<Erp_V1.BLL.CustomerBLL>();
            container.RegisterType<Erp_V1.BLL.ProductBLL>();
            container.RegisterType<Erp_V1.BLL.CategoryBLL>();

            // --- HttpClient and ApiService Registration ---
            container.RegisterType<HttpClient>(new ContainerControlledLifetimeManager(), new InjectionFactory(c =>
            {
                var client = new HttpClient { BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"]) };
                return client;
            }));
            container.RegisterType<Erp.WebApp.Services.Interfaces.IApiService, Erp.WebApp.Services.ApiService>();
        }

        private static IConfiguration BuildConfiguration()
        {
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
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