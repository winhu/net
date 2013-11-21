using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using WinStudio.WebApi.Business;

namespace WinStudio.WebApi
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            var builder = new ContainerBuilder();
            //builder.RegisterType<ContactService>().As<IContactService>();
            builder.RegisterType(typeof(ContactService)).As(typeof(IContactService));
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());
            IContainer container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Autofac.Integration.WebApi.AutofacWebApiDependencyResolver(container);
        }
    }
}