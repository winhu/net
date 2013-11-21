using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace System.Collections.Generic
{
    public static class EnumerableOperation
    {
        public static string ToXml<T>(this IEnumerable<T> lst) where T : class
        {
            if (lst == null || lst.Count() == 0) return string.Empty;
            return lst.ToArray().ToXml();
            //XmlDocument xml = new XmlDocument();
            //XmlElement ele = xml.CreateElement(typeof(T).Name + "s");
            //xml.AppendChild(ele);
            //ele.SetAttribute("Type", lst.GetType().FullName);
            //foreach (T t in lst)
            //{
            //    XmlElement e = xml.CreateElement(typeof(T).Name);
            //    ele.AppendChild(e);
            //    e.SetAttribute("Type", typeof(T).FullName);
            //    xml.CreateElement(
            //}
        }
    }
}
