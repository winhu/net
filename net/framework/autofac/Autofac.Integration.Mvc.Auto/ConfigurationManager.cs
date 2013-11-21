using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Autofac.Integration.Mvc.Auto
{
    public class ConfigurationManager
    {
        static List<KeyValuePair<Type, Type[]>> services = new List<KeyValuePair<Type, Type[]>>();
        public static Type GetServices(Type type)
        {
            KeyValuePair<Type, Type[]> ret = services.Find(kv => kv.Value.Any(t => t == type));
            //if (ret == null) return null;
            return ret.Key;
        }
        public static IDependencyResolver Build(List<Assembly> servassemblies, params Assembly[] ctrlassemblies)
        {
            if (servassemblies == null)
                servassemblies = FindAssemblies();

            services.Clear();
            var builder = new ContainerBuilder();
            servassemblies.ForEach(delegate(Assembly ass)
            {
                UseAutofacAttribute flag = ass.GetCustomAttribute<UseAutofacAttribute>();
                if (flag == null) return;
                foreach (Type type in ass.GetTypes().Where(t => t.IsClass && t.GetCustomAttribute<UseAutofacAttribute>() != null))
                {
                    var itypes = type.GetInterfaces().Where(i => null != i.GetCustomAttribute<UseAutofacAttribute>()).ToArray();

                    builder.RegisterType(type).As(itypes);
                    services.Add(new KeyValuePair<Type, Type[]>(type, itypes));
                }
            });
            builder.RegisterAssemblyTypes(ctrlassemblies);

            IContainer container = builder.Build();
            return new AutofacDependencyResolver(container);
        }

        public static void Build(Type impl, Type[] iscopes, params Assembly[] ctrlassemblies)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(impl).As(iscopes);
            builder.RegisterAssemblyTypes(ctrlassemblies);
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        public static List<KeyValuePair<Type, Type[]>> PickServices(List<Assembly> assemblies)
        {
            if (assemblies == null) assemblies = FindAssemblies();
            services.Clear();
            assemblies.ForEach(delegate(Assembly ass)
            {
                UseAutofacAttribute flag = ass.GetCustomAttribute<UseAutofacAttribute>();
                if (flag == null) return;
                foreach (Type type in ass.GetTypes().Where(t => t.IsClass && t.CustomAttributes.Any(a => a.AttributeType == typeof(UseAutofacAttribute))))
                {
                    var itypes = type.GetInterfaces().Where(i => null != i.GetCustomAttribute<UseAutofacAttribute>()).ToArray();

                    services.Add(new KeyValuePair<Type, Type[]>(type, itypes));
                }
            });
            return services;
        }
        /// <summary>
        /// AutofacConfigurationManager: find all assemblies(*.dll) in current app domain
        /// </summary>
        /// <returns></returns>
        internal static List<Assembly> FindAssemblies()
        {
            var asses = new List<Assembly>();
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            foreach (var file in Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories))
            {
                asses.Add(Assembly.LoadFile(file));
            }
            return asses;
        }
    }
}
