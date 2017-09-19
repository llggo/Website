using System.Web.Mvc;
using System.Web.Routing;

namespace Company
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Details",
                url: "view/{post_name}-{id}",
                defaults: new
                {
                    controller = "Blog",
                    action = "View",
                    post_name = UrlParameter.Optional,
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Company.Controllers" }
            );
            routes.LowercaseUrls = true;
        }
    }
}