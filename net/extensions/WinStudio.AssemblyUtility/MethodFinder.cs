using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Reflection
{
    public static class MethodFinder
    {
        /// <summary>
        /// 返回程序集中所有使用了T的方法集合
        /// </summary>
        /// <typeparam name="T">Attribute</typeparam>
        /// <param name="includeType">如果限制从使用了T的类型中查找方法，则为true；否则为false</param>
        /// <param name="assembly">程序集</param>
        /// <returns></returns>
        public static MethodInfo[] FindMethods<T>(this Assembly assembly, bool includeType = true) where T : Attribute
        {
            if (assembly == null) return null;
            if (includeType)
                return assembly.GetTypes().Where(t => t.Has<T>()).SelectMany(m => m.GetMethods().Where(mi => mi.Has<T>())).ToArray();
            return assembly.GetTypes().SelectMany(m => m.GetMethods().Where(mi => mi.Has<T>())).ToArray();
        }

        /// <summary>
        /// 返回类型中所有使用了T的方法集合
        /// </summary>
        /// <typeparam name="T">Attribute</typeparam>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static MethodInfo[] FindMethods<T>(this Type type) where T : Attribute
        {
            if (type == null) return null;
            return type.GetMethods().Where(m => m.Has<T>()).ToArray();
        }
    }
}
