using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Autofac.Integration.Mef;
using Autofac;
using System.Web.Compilation;

namespace AutofacAutoResolver
{
    public interface IAutofacResolver
    {
        T GetService<T>();
    }

    public class AutofacResolver : IAutofacResolver
    {
        private Autofac.IContainer _container;

        public T GetService<T>()
        {
            if (_container == null || !_container.IsRegistered<T>())
            {
                RegisterPartsFromReferencedAssemblies();
            }
            return _container.Resolve<T>();
        }

        private void RegisterPartsFromReferencedAssemblies()
        {
            var asses = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
            var assemblyCatalogs = asses.Select(x => new AssemblyCatalog(x));
            var catalog = new AggregateCatalog(assemblyCatalogs);

            var builder = new ContainerBuilder();
            builder.RegisterComposablePartCatalog(catalog);
            _container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }

        private static IAutofacResolver _instance = new AutofacResolver();
        public static IAutofacResolver Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
