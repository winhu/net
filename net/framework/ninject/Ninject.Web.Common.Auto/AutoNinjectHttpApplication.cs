using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace Ninject.Web.Common.Auto
{
    /// <summary>
    /// AutoNinjectHttpApplication: inherit from NinjectHttpApplication.
    /// It will bind services which used AutoNinjectAttribute to ninject kernel when application start
    /// </summary>
    public class AutoNinjectHttpApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {

            //DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            //DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));

            var kernel = new StandardKernel();
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
            kernel.Load(Assembly.GetExecutingAssembly());
            BindKernel(kernel);
            return kernel;
        }

        List<KeyValuePair<Type, Type[]>> services = new List<KeyValuePair<Type, Type[]>>();
        private void BindKernel(IKernel kernel)
        {
            services.Clear();
            AutoNinjectServiceBuilder.FindAssemblies().ForEach(delegate(Assembly ass)
            {
                AutoNinjectServiceBuilder.PickServices(ass).ForEach(delegate(AutoNinjectType type)
                {
                    kernel.Bind(type.IServices).To(type.ImplService);
                    services.Add(new KeyValuePair<Type, Type[]>(type.ImplService, type.IServices));
                });
            });
        }

        protected List<KeyValuePair<Type, Type[]>> GetAllService()
        {
            return services;
        }
    }
}
