using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Reflection
{
    public static class Utility
    {
        /// <summary>
        /// 判断当前方法是否具有T类型
        /// T为interface时，含义为该方法是否为T接口中的方法；
        /// T为Attribute时，含义为该方法是否使用了T Attribute；
        /// T为class时，含义为该方法的返回值是否为T类型；
        /// </summary>
        /// <typeparam name="T">支持interface,Attribute,class</typeparam>
        /// <param name="method">方法</param>
        /// <returns></returns>
        public static bool Has<T>(this MethodInfo method)
        {
            Type type = typeof(T);
            if (type.IsInterface)
                return type.GetMethods().Any(m => m.GetType().FullName == method.GetBaseDefinition().GetType().FullName);
            else if (type.IsAttribute())
                return null != method.GetCustomAttribute(type, true);
            else if (type.IsClass)
                return method.ReturnType.FullName == type.FullName;
            else return false;
        }

        /// <summary>
        /// 判断当前类型是否具有T类型
        /// T为interface时，含义为该类型是否继承了T接口；
        /// T为Attribute时，含义为该类型是否使用了T Attribute；
        /// T为class时，含义为该类型是否继承了T类型；
        /// </summary>
        /// <typeparam name="T">支持interface,Attribute,class</typeparam>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool Has<T>(this Type type)
        {
            Type t = typeof(T);
            if (t.IsInterface)
                return type.GetInterfaces().Any(m => m.FullName == t.FullName);
            else if (t.IsAttribute())
                return null != type.GetCustomAttribute(t, true);
            else if (t.IsClass)
                return type.IsSameOrInherited(t.FullName);
            else return false;
        }

        /// <summary>
        /// 判断当前程序集是否具有T类型
        /// T为interface时，含义为该程序集中是否使用了T接口；
        /// T为Attribute时，含义为该程序集是否使用了T Attribute（不做程序集内部类型的判断）；
        /// T为class时，含义为该程序集中是否使用了T类型；
        /// </summary>
        /// <typeparam name="T">支持interface,Attribute,class</typeparam>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool Has<T>(this Assembly assembly)
        {
            Type t = typeof(T);
            if (t.IsInterface)
                return assembly.FindTypes<T>().Length > 0;
            else if (t.IsAttribute())
                return null != assembly.GetCustomAttribute(t);
            else if (t.IsClass)
                return assembly.FindTypes<T>().Length > 0;
            else return false;
        }

        /// <summary>
        /// 判断当前类型是否继承指定类型或与指定类型一致
        /// </summary>
        /// <param name="type">当前类型</param>
        /// <param name="typeName">指定类型全名称</param>
        /// <returns></returns>
        public static bool IsSameOrInherited(this Type type, string typeName)
        {
            if (type.FullName == typeName) return true;
            if (type.FullName == "System.Object" && type.FullName != typeName)
                return false;
            if (type.BaseType == null || type.BaseType.FullName == "System.Object")
                return type.FullName == typeName;
            else return type.BaseType.IsSameOrInherited(typeName);
        }

        /// <summary>
        /// 判断当前类型是否为Attribute类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool IsAttribute(this Type type)
        {
            if (type.BaseType.FullName == "System.Object")
                return type.FullName == "System.Attribute";
            if (type.FullName == "System.Attribute")
                return true;
            else return type.BaseType.IsAttribute();
        }

        /// <summary>
        /// 获取当前域所有使用T的程序集
        /// T为interface时，返回所有使用了T接口的程序集；
        /// T为Attribute时，返回所有使用T Attribute的程序集；
        /// T为class时，返回所有使用了T类型的程序集；
        /// </summary>
        /// <typeparam name="T">支持interface,Attribute,class</typeparam>
        /// <returns></returns>
        public static Assembly[] GetAssembly<T>()
        {
            return AppDomain.CurrentDomain.GetAssemblies().GetAssembly<T>();
        }

        /// <summary>
        /// 获取当前域下的程序集
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns></returns>
        public static Assembly GetAssembly(string assemblyName)
        {
            return GetAssembly().SingleOrDefault(a => a.GetName().Name == assemblyName);
        }

        /// <summary>
        /// 获取当前域下所有的程序集（即*.dll）
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAssembly()
        {
            var asses = new List<Assembly>();
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            foreach (var file in Directory.GetFiles(path))
            {
                if (file.EndsWith(".dll", true, null))
                    asses.Add(Assembly.LoadFile(file));
            }
            return asses.ToArray();
        }
    }
}
