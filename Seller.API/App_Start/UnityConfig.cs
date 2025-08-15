// Removed obsolete Erp.WebApp.Services references
using Erp_V1.BLL;
using Erp_V1.DAL.DAL;
using Seller.API.Services;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using Unity;
using Unity.Lifetime;

namespace Seller.API
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
            var container = new UnityContainer();

            // Register BLLs (Business Logic Layer)
            container.RegisterType<SalesBLL>();

            // Register DbContext - PerRequestLifetimeManager is not available in self-host,
            // so we use HierarchicalLifetimeManager, which works similarly.
            container.RegisterType<DbContext, erp_v2Entities>(new HierarchicalLifetimeManager());
            container.RegisterType<erp_v2Entities>(new HierarchicalLifetimeManager());

            // Register Services needed by the API
            // These service classes (EmailService, InvoicePdfService, WhatsAppService) need to be accessible to this project.
            container.RegisterType<Seller.API.Services.IEmailService, Seller.API.Services.EmailService>(new HierarchicalLifetimeManager());
            container.RegisterType<Seller.API.Services.IInvoicePdfService, Seller.API.Services.InvoicePdfService>(new HierarchicalLifetimeManager());
            container.RegisterType<Seller.API.Services.IWhatsAppService, Seller.API.Services.WhatsAppService>(new HierarchicalLifetimeManager());

            return container;
        }
    }
}