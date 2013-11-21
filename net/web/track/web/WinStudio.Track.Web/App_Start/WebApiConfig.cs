using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WinStudio.Track.Web.WebApi;

namespace WinStudio.Track.Web
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
            config.Formatters.Insert(0, new JsonpMediaTypeFormatter());

            config.MessageHandlers.Add(new AuthenticationHandler());

        }
    }
}
