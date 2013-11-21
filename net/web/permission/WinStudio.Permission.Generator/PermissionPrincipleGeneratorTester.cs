using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Generator
{
    public class PermissionPrincipleGeneratorTester
    {
        public void Run()
        {
            Generator generator = new Generator();
            var tree = generator.Run("Synjones.Dreams.Plugins.Card", "Card");
            Print(tree.GetFunctions());
        }

        private void Print(List<Func> tree, string level = "")
        {
            foreach (var func in tree)
            {
                Console.WriteLine("{0}{1},{2},{3},{4},{5}", level, func.Name, func.Area, func.Controller, func.Action, func.Display);
                Print(func.Sons, level + "---");
            }
        }
    }
}
