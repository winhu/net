using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace System.Collections.Generic
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
        /// remove empty or null value
        /// </summary>
        /// <param name="lst">array</param>
        /// <returns></returns>
        public static List<T> RemoveNullorEmpty<T>(this List<T> lst)
        {
            if (lst.HasValue())
                return lst.Where(t => t != null && !string.IsNullOrEmpty(t.ToString())).ToList();
            return lst;
        }
        public static string ToXml<T>(this IEnumerable<T> lst) where T : class
        {
            if (lst == null || lst.Count() == 0) return string.Empty;
            return ToXml(lst.ToArray());
        }
        /// <summary>
        /// Serialize object to xml string
        /// </summary>
        /// <param name="o">object</param>
        /// <param encoding="o">encoding</param>
        /// <returns></returns>
        private static string ToXml(object o, string encoding = "utf-8")
        {
            if (o == null)
            {
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(o.GetType());
                MemoryStream stream = new MemoryStream();
                Encoding _encoding = Encoding.GetEncoding(encoding);
                XmlTextWriter writer = new XmlTextWriter(stream, _encoding);
                x.Serialize(writer, o);
                byte[] utf8EncodedData = stream.GetBuffer();
                return _encoding.GetString(utf8EncodedData, 0, (int)stream.Length);
            }
            return string.Empty;
        }

        /// <summary>
        /// join item with char
        /// </summary>
        /// <param name="o">array</param>
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

        /// <summary>
        /// join item with char
        /// </summary>
        /// <param name="o">array</param>
        /// <param name="c">char</param>
        /// <param name="removenullorempty">need to remove null or empty value</param>
        /// <returns></returns>
        public static string Join<T>(this List<T> lst, char c = '|', bool removenullorempty = true)
        {
            if (lst.HasValue())
            {
                var a = lst;
                if (removenullorempty)
                    a = lst.RemoveNullorEmpty();

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
