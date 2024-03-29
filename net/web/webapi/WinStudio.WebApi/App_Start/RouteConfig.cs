﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.ApplicationServer.Http.Activation;
using WinStudio.WebApi.Wcf;

namespace WinStudio.WebApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            //var config = new Microsoft.ApplicationServer.Http.HttpConfiguration() { EnableTestClient = true };
            //routes.Add(new ServiceRoute("api/contact", new HttpServiceHostFactory(), typeof(ContactApi)));
        }
    }
}