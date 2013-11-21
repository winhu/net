using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Principle.Mvc
{
    public class PermissionPrincipleGeneratorTester
    {
        public void Run()
        {
            Generator generator = new Generator();
            var tree = generator.Run("Synjones.Dreams.Plugins.Card", "Card");
            Print(tree);
            tree.ToXml("c:\\permissiontree.xml");
        }
        public void Load()
        {
            Generator generator = new Generator();
            var tree = generator.LoadPermissionTree("c:\\permissiontree.xml");
            if (tree == null) Console.WriteLine("load from xml failed");
            Print(tree);
        }

        private void Print(IPermissionTree tree, string level = "")
        {
            Console.WriteLine(tree.Name);
            Print(tree.GetNodes(PermissionNodeType.Virtual), "");
        }
        private void Print(List<IPermissionNode> nodes, string level = "")
        {
            foreach (var node in nodes)
            {
                Console.WriteLine("{0}{1},{2},{3},{4}", level, node.Name, node.Code, node.Address, node.Display);
                Print(node.Children, level + "---");
            }
        }
    }
}
