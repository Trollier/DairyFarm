using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DairyFarm.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Cattle", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
             "Editdisease",                                              // Route name
            "{controller}/{action}/{idDiseaseHistory}/{idCattle}",                           // URL with parameters
            new { controller = "DiseasesHistories", action = "Edit", idDiseaseHistory = "", idCattle = "" }  // Parameter defaults
        );

        }
    }
}
