using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.RabittMQClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQ.Client.Examples.AddServer.Run(new string[] { "<uri> = \"amqp://user:pass@host:port/vhost\"" });
        }
    }
}
