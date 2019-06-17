using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Profile
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Product Category",
                url: "chi-tiet/{metatitle}-{detailProductsID}",
                defaults: new { controller = "DetailProducts", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "Profile.Controllers" }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Add Cart",
                url: "them-gioi-hang",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "Profile.Controllers" }
            );
            //Router name Default luôn lấy mặc định sau cùng nếu không có các Router đứng phía trước đó.
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Profile.Controllers" }
            );
            
        }
    }
}
