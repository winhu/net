using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class Operation
    {
        /// <summary>
        /// Performs the specified action on each element of the System.Array.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="arr">System.Array</param>
        /// <param name="action">The System.Action delegate to perform on each element of the System.Array.</param>
        public static void ForEach<T>(this T[] arr, Action<T> action)
        {
            foreach (T t in arr)
                action(t);
        }

        /// <summary>
        /// is t in T[]
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="arr">System.Array</param>
        /// <param name="t">t</param>
        /// <returns></returns>
        public static bool Has<T>(this T[] arr, T t)
        {
            return arr.Contains(t);
        }

        /// <summary>
        /// remove empty or null value
        /// </summary>
        /// <param name="arr">array</param>
        /// <returns></returns>
        public static T[] RemoveNullorEmpty<T>(this T[] arr)
        {
            if (arr.HasValue())
                return arr.Where(t => t != null && !string.IsNullOrEmpty(t.ToString())).ToArray();
            return arr;
        }

        /// <summary>
        /// join item with char
        /// </summary>
        /// <param name="arr">array</param>
        /// <param name="c">char</param>
        /// <param name="removenullorempty">need to remove null or empty value</param>
        /// <returns></returns>
        public static string Join<T>(this T[] arr, char c = '|', bool removenullorempty = true)
        {
            if (arr.HasValue())
            {
                var a = arr;
                if (removenullorempty)
                    a = arr.RemoveNullorEmpty();

                if (a.HasValue())
                {
                    StringBuilder sb = new StringBuilder();
                    a.ForEach(delegate(T t)
                    {
                        if (sb.Length > 0) sb.Append(c);
                        sb.Append(t);
                    });
                    return sb.ToString();
                }
            }
            return string.Empty;
        }
    }
}
