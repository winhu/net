using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System.Reflection
{
    public static class TypeFinder
    {
        /// <summary>
        /// 获取程序集中所有使用T的类型。
        /// T为interface时，返回所有继承该interface的类型；
        /// T为Attribute时，返回所有使用该Attribute的类型；
        /// T为class时，返回所有继承该class的类型；
        /// 否则返回空集合。
        /// </summary>
        /// <typeparam name="T">支持interface,Attribute,class</typeparam>
        /// <param name="assembly">程序集</param>
        /// <returns>aa</returns>
        public static Type[] FindTypes<T>(this Assembly assembly)
        {
            if (assembly == null) return new Type[] { };
            Type type = typeof(T);
            if (type.IsInterface)
                return assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.FullName == type.FullName)).ToArray();
            else if (type.IsAttribute())
                return assembly.GetTypes().Where(t => t.Has<T>()).ToArray();
            else if (type.IsClass)
                return assembly.GetTypes().Where(t => t.IsSameOrInherited(type.FullName)).ToArray();
            else return new Type[] { };
        }

        /// <summary>
        /// 获取当前程序集内所有指定类型
        /// </summary>
        /// <param name="assembly">当前程序集</param>
        /// <param name="typeFullName">指定类型</param>
        /// <returns></returns>
        public static Type[] FindTypes(this Assembly assembly, string typeFullName)
        {
            return assembly.FindTypes(typeFullName);
        }

        /// <summary>
        /// 获取当前集合内所有使用了制定类型的类型
        /// </summary>
        /// <typeparam name="T">制定类型</typeparam>
        /// <param name="assemblies">当前集合</param>
        /// <returns></returns>
        public static Type[] FindTypes<T>(this Assembly[] assemblies)
        {
            return assemblies.SelectMany(a => a.FindTypes<T>()).ToArray();
        }

        /// <summary>
        /// 获取当前集合所有的制定类型
        /// </summary>
        /// <param name="assemblies">当前集合</param>
        /// <param name="typeFullName">制定类型</param>
        /// <returns></returns>
        public static Type[] FindTypes(this Assembly[] assemblies, string typeFullName)
        {
            return assemblies.SelectMany(a => a.GetTypes().FindTypes(typeFullName)).ToArray();
        }

        /// <summary>
        /// 获取Type数组中所有使用T的类型。
        /// T为interface时，返回所有继承该interface的类型；
        /// T为Attribute时，返回所有使用该Attribute的类型；
        /// T为class时，返回所有继承该class的类型；
        /// 否则返回空集合。
        /// </summary>
        /// <typeparam name="T">支持interface,Attribute,class</typeparam>
        /// <param name="types">Type数组</param>
        /// <returns></returns>
        public static Type[] FindTypes<T>(this Type[] types)
        {
            if (types == null) return new Type[] { };
            Type type = typeof(T);
            if (type.IsInterface)
                return types.Where(t => t.GetInterfaces().Any(i => i.Name == type.Name)).ToArray();
            else if (type.IsAttribute())
                return types.Where(t => t.Has<T>()).ToArray();
            else if (type.IsClass)
                return types.Where(t => t.IsSameOrInherited(type.FullName)).ToArray();
            else return new Type[] { };
        }

        /// <summary>
        /// 获取当前集合所有使用了制定类型的类型
        /// </summary>
        /// <param name="types">当前集合</param>
        /// <param name="typeFullName">制定类型</param>
        /// <returns></returns>
        public static Type[] FindTypes(this Type[] types, string typeFullName)
        {
            return types.Where(t => t.IsSameOrInherited(typeFullName)).ToArray();
        }
    }
}
