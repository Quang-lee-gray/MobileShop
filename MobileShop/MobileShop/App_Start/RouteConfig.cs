using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MobileShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
               name: "Home",
               url: "trangchu",
               defaults: new { controller = "Home", action = "Index", seoTitle = UrlParameter.Optional },
               namespaces: new[] { "MobileShop.Controllers" }
           );
            routes.MapRoute(
               name: "ShoppingCart",
               url: "gio-hang",
               defaults: new { controller = "ShoppingCart", action = "Index", seoTitle = UrlParameter.Optional },
               namespaces: new[] { "MobileShop.Controllers" }
           );
            routes.MapRoute(
             name: "CheckOut",
             url: "thanh-toan",
             defaults: new { controller = "ShoppingCart", action = "CheckOut", seoTitle = UrlParameter.Optional },
             namespaces: new[] { "MobileShop.Controllers" }
         );
            routes.MapRoute(
               name: "Contact",
               url: "lienhe",
               defaults: new { controller = "Contact", action = "Index", seoTitle = UrlParameter.Optional },
               namespaces: new[] { "MobileShop.Controllers" }
           );
            routes.MapRoute(
               name: "CategoryProduct",
               url: "danh-muc-san-pham/{seoTitle}-{id}",
               defaults: new { controller = "Product", action = "ProductCategory", seoTitle = UrlParameter.Optional },
               namespaces: new[] { "MobileShop.Controllers" }
           );
            routes.MapRoute(
                name: "Product",
                url: "chi-tiet-san-pham/{seoTitle}-p{id}",
                defaults: new { controller = "Product", action = "DetailsProduct", seoTitle = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );
            routes.MapRoute(
               name: "DetailsProduct",
               url: "sanpham",
               defaults: new { controller = "Product", action = "Index", seoTitle = UrlParameter.Optional },
               namespaces: new[] { "MobileShop.Controllers" }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );
        }
    }
}
