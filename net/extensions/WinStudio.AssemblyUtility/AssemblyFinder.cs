using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Reflection
{
    public static class AssemblyFinder
    {
        /// <summary>
        /// 获取当前集合所有包含指定类型名的程序集
        /// </summary>
        /// <param name="typeName">指定类型名</param>
        /// <returns></returns>
        public static Assembly[] GetAssembly(this Assembly[] assemblies, string typeName)
        {
            return assemblies.Where(a => a.GetTypes().Any(t => t.IsSameOrInherited(typeName))).ToArray();
        }

        /// <summary>
        /// 获取当前集合中所有使用T的程序集
        /// T为interface时，返回所有使用了T接口的程序集；
        /// T为Attribute时，返回所有使用T Attribute的程序集；
        /// T为class时，返回所有使用了T类型的程序集；
        /// </summary>
        /// <typeparam name="T">支持interface,Attribute,class</typeparam>
        /// <param name="assembly">当前程序集集合</param>
        /// <returns></returns>
        public static Assembly[] GetAssembly<T>(this Assembly[] assemblies)
        {
            return assemblies.Where(a => a.Has<T>()).ToArray();
        }

    }
}
