using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LB8
{
    public static class WebApiConfig
    {
        public static string UrlPrefix { get { return "api"; } }
        public static string UrlPrefixRelative { get { return "~/api"; } }
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: UrlPrefix + "/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
