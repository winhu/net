using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Ninject.Web.Common.Auto
{
    /// <summary>
    /// AutoNinjectServiceBuilder: pick services from assemblies in current app domain
    /// </summary>
    public class AutoNinjectServiceBuilder
    {
        /// <summary>
        /// AutoNinjectServiceBuilder: pick services from assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        internal static List<AutoNinjectType> PickServices(Assembly assembly)
        {
            List<AutoNinjectType> lst = new List<AutoNinjectType>();
            AutoNinjectAttribute flag = assembly.GetCustomAttribute<AutoNinjectAttribute>();
            if (flag == null) return lst;
            foreach (Type type in assembly.GetTypes().Where(t => t.IsClass && t.CustomAttributes.Any(a => a.AttributeType == typeof(AutoNinjectAttribute))))
            {
                var itypes = type.GetInterfaces().Where(i => null != i.GetCustomAttribute<AutoNinjectAttribute>()).ToArray();
                if (itypes.Length > 0)
                    lst.Add(new AutoNinjectType { ImplService = type, IServices = itypes });
            }
            return lst;
        }

        /// <summary>
        /// AutoNinjectServiceBuilder: find all assemblies(*.dll) in current app domain
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
