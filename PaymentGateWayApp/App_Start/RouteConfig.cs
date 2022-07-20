using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PaymentGateWayApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            ///*   constraints :new
            //   { 
            //       serverRoute = new ServerRouteConstraint(url =>{

            //           return url.PathAndQuery.StartsWith("/Filter/", StringComparison.InvariantCultureIgnoreCase);

            //       })

            //   } */

            //);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
          

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Demo",
                    action = "Demo",
                    id = UrlParameter.Optional
                }
                );


         
            //constraints: new
            //{
            //    serverRoute = new ServerRouteConstraint(url =>
            //    {

            //        return url.PathAndQuery.StartsWith("/Filter/", StringComparison.InvariantCultureIgnoreCase);

            //    })

            //}


        }
    }
}
