using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // ALL THE PROPERTIES:
            // rentalProperties/
            routes.MapRoute(
                name: "Products",
                url: "Products/{action}/{id}",
                defaults: new
                {
                    controller = "Products",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
