using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Ecommerce_CubicTaks_.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Global error handler
            config.Filters.Add(new GlobalExceptionFilter());
            //Log all requests
            config.MessageHandlers.Add(new LoggingHandler());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
