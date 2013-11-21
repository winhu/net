using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Thinktecture.Web.Http.Formatters.API;

namespace WinStudio.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //注册JsonpMediaTypeFormatter
            var webapiconfig = GlobalConfiguration.Configuration;
            webapiconfig.Formatters.Insert(0, new JsonpMediaTypeFormatter());
        }
    }
}
