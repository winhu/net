using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WinStudio.Permission.Principle.Mvc
{
    public class Generator
    {
        public IPermissionTree Run(string assemblyname, string area)
        {
            var ass = Utility.GetAssembly(assemblyname);
            return Run(ass, area);
        }

        public bool Do(string assemblyname, string area, string path)
        {
            try
            {
                var ass = Utility.GetAssembly(assemblyname);
                IPermissionTree tree = Run(ass, area);
                if (tree == null) throw new Exception("Generator failed. please check your dll and your input.");
                tree.ToXml(path);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public IPermissionTree LoadPermissionTree(string filename)
        {
            try
            {
                IPermissionTree tree = new PermissionTree();
                tree.LoadXml(filename);
                return tree;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// 根据程序集和域得到所有功能点
        /// </summary>
        /// <param name="assemblyname">程序集名称</param>
        /// <param name="area">域名称</param>
        /// <returns></returns>
        public IPermissionTree Run(Assembly assemblies, string area)
        {
            var tree = new PermissionTree(area);
            var funcs = new List<IPermissionNode>();
            foreach (var type in assemblies.FindTypes<NeedAutoPermissionAttribute>())
            {
                NeedAutoPermissionAttribute cattr = type.GetCustomAttribute<NeedAutoPermissionAttribute>(true);
                string ctrl = type.Name.Replace("Controller", "");
                string fname = string.IsNullOrEmpty(cattr.Name) ? ctrl : cattr.Name;
                IPermissionNode parent = PermissionNode.CreateNodeType(PermissionNodeType.Function, fname, ctrl, null, cattr.Display, string.Format("{0}/{1}", area.Trim(), ctrl));
                tree.AddPermissionNode(parent);
                foreach (var method in type.FindMethods<NeedAutoPermissionAttribute>())
                {
                    NeedAutoPermissionAttribute aattr = method.GetCustomAttribute<NeedAutoPermissionAttribute>(true);
                    foreach (KeyValuePair<string, string> role in aattr.GetRoles())
                    {
                        tree.AddPermissionNode(PermissionNode.CreateNodeType(PermissionNodeType.Role, role.Value, role.Key, role.Key, aattr.Display));
                    }
                    IPermissionNode node = PermissionNode.CreateNodeType(PermissionNodeType.Function, string.IsNullOrEmpty(aattr.Name) ? method.Name : aattr.Name, string.Format("{0}.{1}", ctrl, method.Name), aattr.IsNativeAction(ctrl) ? ctrl : aattr.Parent, aattr.Display, string.Format("{0}/{1}/{2}", area.Trim(), ctrl, method.Name));//
                    tree.AddPermissionNode(node);
                }
            }
            tree.PickupPieces();
            return tree;
        }
    }
}
