﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hospital
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
       {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Genres",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Genre", action = "GetGenres", id = UrlParameter.Optional }
            //);

         //   routes.MapRoute(
         //    name: "Login",
         //    url: "{controller}/{action}/{id}",
         //    defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
         //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
