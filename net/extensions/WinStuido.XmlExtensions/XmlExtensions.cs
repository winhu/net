using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
    public static class XmlExtensions
    {
        public static FileInfo ToFile(this string s)
        {
            if (string.IsNullOrEmpty(s) || !File.Exists(s))
                throw new Exception(string.Format("file path '{0}' is error!", s));
            return new FileInfo(s);
        }

        public static StreamReader ToStreamReader(this string s)
        {
            return s.ToFile().OpenText();
        }

        public static XmlDocument ToXmlDocument(this string s)
        {
            if (string.IsNullOrEmpty(s) || !File.Exists(s))
                throw new Exception(string.Format("file path '{0}' is error!", s));
            XmlDocument xml = new XmlDocument();
            xml.Load(s);
            return xml;
        }

        public static XmlDocument ToXmlDocument(this StreamReader sr)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(sr);
            return xml;
        }

        public static XmlNode GetNode(this XmlDocument doc, string name)
        {
            if (doc == null) return null;
            XmlNode node = doc.FirstChild;
            while (node != null)
            {
                var n = node.GetNode(name);
                if (null != n)
                    return n;
                node = node.NextSibling;
            }
            return null;
        }

        public static XmlNode GetNode(this XmlNode node, string name)
        {
            if (node == null) return null;
            if (node.Name == name) return node;
            foreach (XmlNode child in node.ChildNodes)
            {
                if (null != child.GetNode(name))
                    return child;
            }
            return null;
        }

        public static List<XmlNode> GetNodes(this XmlDocument doc, string name)
        {
            List<XmlNode> ret = new List<XmlNode>();
            if (doc == null) return ret;
            XmlNode node = doc.FirstChild;
            if (node == null) return ret;
            while (node != null)
            {
                ret.AddRange(node.GetNodes(name));
                node = node.NextSibling;
            }
            return ret;
        }

        public static List<XmlNode> GetNodes(this XmlNode node, string name)
        {
            List<XmlNode> nodes = new List<XmlNode>();
            if (node == null) return nodes;
            if (node.Name == name) nodes.Add(node);
            else
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    var lst = child.GetNodes(name);
                    if (lst.Count > 0)
                        nodes.AddRange(lst);
                }
            }
            return nodes;
        }

        public static bool IsLeafElement(this XmlNode node)
        {
            if (node == null) return false;
            if (!node.HasChildNodes) return false;
            foreach (XmlNode child in node.ChildNodes)
                if (!child.IsLeaf()) return false;
            return true;
        }

        public static bool IsLeaf(this XmlNode node, XmlNodeType type = XmlNodeType.Text)
        {
            if (node == null) return false;
            return node.HasChildNodes && node.ChildNodes.Count == 1 && node.FirstChild.NodeType == type;
        }

        public static string GetChildValue(this XmlNode node, string name)
        {
            if (!node.IsLeafElement()) return string.Empty;
            XmlNode n = node.GetNode(name);
            if (n == null) return string.Empty;
            return n.IsLeaf() ? n.FirstChild.Value : string.Empty;
        }

        public static string GetNodeAttributeValue(this XmlNode node, string name, string attr)
        {
            if (!node.IsLeafElement()) return string.Empty;
            XmlNode n = node.GetNode(name);
            if (n == null) return string.Empty;
            return n.Attributes[attr].Value;
        }
    }

    public class tester
    {
        public void test()
        {
            string path = "c:\\testxml.xml";
            XmlDocument xml = path.ToXmlDocument();
            var node = xml.GetNodes("Student");
            Console.WriteLine(node[0].GetChildValue("Name"));
        }
    }
}
