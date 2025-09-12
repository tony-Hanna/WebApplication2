using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApplication2.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable attribute routing
            config.MapHttpAttributeRoutes();

            // Default route: api/{controller}/{id}
            config.Routes.MapHttpRoute(
                name: "DefaultApi", //unique name for the router
                routeTemplate: "api/{controller}/{id}", // usersController - controller = users
                defaults: new { id = RouteParameter.Optional } //id is optional
            );
        }
    }
}