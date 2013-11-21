using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Principle.Mvc
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = "";
            Generator generator = new Generator();
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("welcome to use winstudio.permission.generator.");
            Console.WriteLine("this program is used to generator xml file, so ");
            Console.WriteLine("please make sure your dll followed the principle.");
            Console.WriteLine("then follow the tip to generate and");
            Console.WriteLine("press 'quit' to exit.");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("area name: ");
            string area = string.Empty;
            string path = string.Empty;
            while ((line = Console.ReadLine()) != "quit")
            {
                if (string.IsNullOrEmpty(line)) continue;
                if (string.IsNullOrEmpty(area))
                {
                    area = line;
                    Console.WriteLine("assembly name[without '.dll' and attention the capital]:");
                    continue;
                }
                if (string.IsNullOrEmpty(path))
                {
                    path = line;

                    Console.WriteLine("-----please wait for a while...");
                    Console.WriteLine();
                    var ret = generator.Do(path, area, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, line + ".xml"));
                    area = string.Empty;
                    path = string.Empty;

                    if (ret) Console.WriteLine("please check the xml file: " + line + ".xml");
                    Console.WriteLine();
                    Console.WriteLine("do again.");
                    Console.WriteLine("please input area: ");
                }
            }
        }
    }
}
