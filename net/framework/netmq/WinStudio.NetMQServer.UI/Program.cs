using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.NetMQServer.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker.Instance.Init("localhost", 5555);

            Console.ReadKey();
        }
    }
}
