using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.Mef;
using Autofac.Integration.Mvc.Auto;
using WinStudio.Framework.Authentication;
using WinStudio.Permission.Models;
using WinStudio.TestDataFramework.EF;
using WinStudio.Permission.Web.Controllers;
using WinStudio.Framework.Web.Services;

namespace WinStudio.Permission.Web
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
            AuthConfig.RegisterAuth();

            new TestDataRuner<PermissionDbContext>().Run();
            WinWebGlobalConfiguration.Initialize();
            //var ass = Assembly.GetAssembly(typeof(HomeController));
            //var container = RegisterPartsFromReferencedAssemblies(ass);
            //AutofacResolver.SetAutofacBusiContainer(container);
            //var builder = new ContainerBuilder();
            //RegisterPartsFromReferencedAssemblies(builder);
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());
            //IContainer container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}