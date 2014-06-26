using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace WinStudio.NetMQClient
{
    public class Demo
    {
        private string _address = "localhost";
        private int _port = 5555;
        public void StartClient()
        {
            Console.WriteLine("I am Client");
            using (NetMQContext context = NetMQContext.Create())
            {
                using (var client = context.CreateRequestSocket())
                {
                    client.Connect(string.Format("tcp://{0}:{1}", _address, _port));
                    //client.Send("FIRST");
                    client.ReceiveReady += client_ReceiveReady;
                    client.Send("Client");
                    //client.Poll();
                }

            }
            Console.ReadKey();
        }

        void client_ReceiveReady(object sender, NetMQSocketEventArgs e)
        {
            Guid guid;
            string ret = e.Socket.ReceiveString();//false, out hasMore);

            Guid.TryParse(ret, out guid);
            if (guid == Guid.Empty)
            {
                Console.WriteLine("null guid");
                return;
            }
            Console.WriteLine(guid);
            using (RouterSocket router = NetMQContext.Create().CreateRouterSocket())
            {
                router.Bind(string.Format("tcp://{0}:{1}", _address, 6666));
                router.Options.Linger = TimeSpan.Zero;
                router.ReceiveReady += router_ReceiveReady;
                //router.Options.Identity = Encoding.UTF8.GetBytes(ret);
                router.SendMore(Encoding.UTF8.GetBytes(ret));
                router.Send("hello");
            }

        }

        void router_ReceiveReady(object sender, NetMQSocketEventArgs e)
        {
            string ret = e.Socket.ReceiveString();//false, out hasMore);
            if (string.IsNullOrEmpty(ret))
            {
                Console.WriteLine("null response");
                return;
            }
            Console.WriteLine(ret);

        }
    }
}
