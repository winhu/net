using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc.Auto;
using WinStudio.Framework.Authentication;
using WinStudio.Framework.Web.Services;
using WinStudio.Track.Models;
using WinStudio.Track.Web.Controllers;

namespace WinStudio.Track.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : WinAutofacHttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = true;

            WinWebGlobalConfiguration.Initialize();


            //var ass = Assembly.GetAssembly(typeof(HomeController));
            //var container = RegisterPartsFromReferencedAssemblies(ass);
            //AutofacResolver.SetAutofacBusiContainer(container);

        }

    }
}