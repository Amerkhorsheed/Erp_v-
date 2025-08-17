using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Erp.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Notification specific routes
            routes.MapRoute(
                name: "NotificationDetails",
                url: "Notifications/Details/{id}",
                defaults: new { controller = "Notifications", action = "Details" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: "NotificationMarkAsRead",
                url: "Notifications/MarkAsRead/{id}",
                defaults: new { controller = "Notifications", action = "MarkAsRead" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: "NotificationDelete",
                url: "Notifications/Delete/{id}",
                defaults: new { controller = "Notifications", action = "Delete" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                // --- THIS IS THE LINE TO CHANGE ---
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}