using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
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
using WinStudio.Demo.WebMvc.ContactBusiness;

namespace WinStudio.Demo.WebMvc
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //var resolver = ConfigurationManager.Build(null, Assembly.GetExecutingAssembly());
            //DependencyResolver.SetResolver(resolver);
            //List<IContactorProxy> ret = DependencyResolver.Current.GetServices<IContactorProxy>().ToList();

            //var services = ConfigurationManager.PickServices(null);

            var builder = new ContainerBuilder();
            //foreach (KeyValuePair<Type, Type[]> kv in services)
            //{
            //    builder.RegisterType(kv.Key).As(kv.Value);
            //}
            //Assembly ass = Assembly.GetAssembly(typeof(ContactorProxy));
            //foreach (Type type in ass.GetTypes().Where(t => t.IsClass && t.GetCustomAttribute<UseAutofacAttribute>() != null))
            //{
            //    var itypes = type.GetInterfaces().Where(i => null != i.GetCustomAttribute<UseAutofacAttribute>()).ToArray();
            //    builder.RegisterType(type).As(itypes);
            //}

            //builder.RegisterType(con).As(icon);
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            RegisterPartsFromReferencedAssemblies(builder);
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            IContactorProxy proxy = container.Resolve<IContactorProxy>();

            List<IContactorProxy> ret = DependencyResolver.Current.GetServices<IContactorProxy>().ToList();
        }

        // Example method to register assemblies with ContainerBuilder
        public void RegisterPartsFromReferencedAssemblies(ContainerBuilder builder)
        {
            // Get referenced assemblies
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();

            // Create an AssemblyCatalog from each assembly
            var assemblyCatalogs = assemblies.Select(x => new AssemblyCatalog(x));

            // Combine all AssemblyCatalogs into an AggregateCatalog
            var catalog = new AggregateCatalog(assemblyCatalogs);

            // Register the catalog with the ContainerBuilder
            builder.RegisterComposablePartCatalog(catalog);
        }
    }
}