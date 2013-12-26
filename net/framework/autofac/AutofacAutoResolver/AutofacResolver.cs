using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Autofac;
using System.Web.Compilation;

namespace AutofacAutoResolver
{
    /// <summary>
    /// 适用于MVC框架（实例维护在当前请求中，亦可使用DependencyResolver直接解析服务）
    /// </summary>
    public interface IAutofacResolver
    {
        /// <summary>
        /// 获取所需服务
        /// </summary>
        /// <typeparam name="T">服务接口</typeparam>
        /// <returns></returns>
        T GetService<T>();

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="assemblies">服务所在的程序集（段名称）</param>
        void RegisterServices(params string[] assemblies);

        /// <summary>
        /// 注册控制器
        /// </summary>
        /// <param name="assemblies">控制器所在的程序集（段名称）</param>
        void RegisterControllers(params string[] assemblies);
    }

    public class AutofacResolver : IAutofacResolver
    {
        private bool _initialized = false;

        private string[] _controller_assemblies;
        private string[] _service_assemblies;

        public T GetService<T>()
        {
            if (!_initialized)
                Initialize();
            return DependencyResolver.Current.GetService<T>();
        }

        public void RegisterServices(params string[] assemblies)
        {
            _service_assemblies = assemblies;
        }

        private void Initialize()
        {
            if (!_initialized)
            {
                ContainerBuilder builder = new ContainerBuilder();

                var asms = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();
                var asms_ctrl = asms.GetAssembly<AutofacControllerFlagAttribute>();
                var asms_serv = asms.GetAssembly<AutofacServiceFlagAttribute>();


                builder.RegisterControllers(asms_ctrl).InstancePerHttpRequest();

                foreach (var asm in asms_serv)
                {
                    foreach (var type in asm.FindTypes<AutofacServiceFlagAttribute>())
                    {
                        Type i = type.GetCustomAttribute<AutofacServiceFlagAttribute>().Service;
                        builder.RegisterType(type).As(type.GetCustomAttribute<AutofacServiceFlagAttribute>().Service);
                    }
                }

                IContainer container = builder.Build();
                DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
                _initialized = true;
            }
        }

        private Assembly[] GetReferencedAssemblies(params string[] assemblyNames)
        {
            return BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray().Where(a => assemblyNames.Contains(a.GetName().Name)).ToArray();
        }

        public void RegisterControllers(params string[] assemblies)
        {
            _controller_assemblies = assemblies;
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

    /*
    public class AutofacResolver : IAutofacResolver
    {
        private Autofac.IContainer _container;

        public T GetService<T>(params Assembly[] assemblies)
        {
            if (_container == null || !_container.IsRegistered<T>())
            {
                RegisterPartsFromReferencedAssemblies(assemblies);
            }
            return DependencyResolver.Current.GetService<T>();
            //return _container.Resolve<T>();
        }

        private void RegisterPartsFromReferencedAssemblies(params Assembly[] assemblies)
        {
            var asses = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
            var assemblyCatalogs = asses.Select(x => new AssemblyCatalog(x));
            var catalog = new AggregateCatalog(assemblyCatalogs);

            var builder = new ContainerBuilder();
            builder.RegisterComposablePartCatalog(catalog);
            builder.RegisterControllers(assemblies).InstancePerDependency();
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

     */
}
