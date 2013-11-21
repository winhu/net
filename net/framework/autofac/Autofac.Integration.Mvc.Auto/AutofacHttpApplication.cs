using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using Autofac.Integration.Mef;

namespace Autofac.Integration.Mvc.Auto
{
    public class WinAutofacHttpApplication : HttpApplication
    {

        // Example method to register assemblies with ContainerBuilder
        public IContainer RegisterPartsFromReferencedAssemblies(params Assembly[] assemblies)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assemblies);
            // Get referenced assemblies
            var asses = BuildManager.GetReferencedAssemblies().Cast<Assembly>();

            // Create an AssemblyCatalog from each assembly
            var assemblyCatalogs = asses.Select(x => new AssemblyCatalog(x));

            // Combine all AssemblyCatalogs into an AggregateCatalog
            var catalog = new AggregateCatalog(assemblyCatalogs);

            // Register the catalog with the ContainerBuilder
            builder.RegisterComposablePartCatalog(catalog);
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            return container;
        }
    }
}
