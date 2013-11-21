using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WinStudio.Permission.Principle.Mvc
{
    public static class extensions
    {
        internal static bool ToBoolean(this string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            bool ret = false;
            Boolean.TryParse(s, out ret);
            return ret;
        }
    }

    public class PermissionLoader
    {
        private XmlDocument doc = null;
        public IPermissionTree Loader(string filepath)
        {
            try
            {
                string xml = File.ReadAllText(filepath);
                doc.LoadXml(xml);
                XmlElement root = doc.DocumentElement;
                IPermissionTree tree = new PermissionTree(root.Attributes["Area"].ToString());
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.Name == "Roles")
                    {
                        foreach (XmlNode role in node.ChildNodes)
                        {
                            tree.AddPermissionNode(
                                PermissionNode.CreateNodeType(
                                PermissionNodeType.Role,
                                role.Attributes["Name"].ToString(),
                                role.Attributes["Code"].ToString(), null,
                                role.Attributes["Display"].ToString().ToBoolean()));
                        }
                    }
                    else if (node.Name == "Functions")
                    {
                        foreach (XmlNode func in node.ChildNodes)
                        {
                            tree.AddPermissionNode(
                                PermissionNode.CreateNodeType(
                                PermissionNodeType.Function,
                                func.Attributes["Name"].ToString(),
                                func.Attributes["Code"].ToString(),
                                func.Attributes["Code"].ToString(),
                                func.Attributes["Display"].ToString().ToBoolean(),
                                func.Attributes["Address"].ToString()));
                        }
                    }
                }
                tree.PickupPieces();
                return tree;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
