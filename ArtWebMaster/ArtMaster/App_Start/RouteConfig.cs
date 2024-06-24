using System.Web.Mvc;
using System.Web.Routing;

namespace ArtMaster
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Dashboard",
               url: "Reports/Dashboard",
               defaults: new { controller = "Reports", action = "Dashboard", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "QuickView",
               url: "Reports/QuickView",
               defaults: new { controller = "Reports", action = "Dashboard", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
            );

        }
    }
}
