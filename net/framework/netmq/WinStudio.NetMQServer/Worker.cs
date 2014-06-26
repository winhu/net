using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace WinStudio.NetMQServer
{
    public class Worker
    {
        private static Worker _instance = new Worker();
        public static Worker Instance { get { return _instance; } }

        public Guid WorkerId { get; private set; }
        public string Name { get; set; }
        public NetMQContext Context { get; private set; }

        private IDictionary<Guid, NetMQSocket> _dealer_sockets = new Dictionary<Guid, NetMQSocket>();
        private ResponseSocket _response_socket;
        public Worker()
        {
            Name = "Worker";
            Context = NetMQContext.Create();
            WorkerId = Guid.NewGuid();
        }
        private static string SERVER_ENDPOINT = "tcp://127.0.0.1:5555";
        private string _address = "192.168.11.64";
        private int _port1 = 5555;

        public void Init(string address, int port)
        {
            Console.WriteLine("binding to " + SERVER_ENDPOINT);
            _address = address;
            //_port = port;
            _response_socket = Context.CreateResponseSocket();
            _response_socket.Bind(SERVER_ENDPOINT);//string.Format("tcp://{0}:{1}", address, _port1));
            _response_socket.Options.Identity = Encoding.UTF8.GetBytes("S0");
            _response_socket.ReceiveReady += _response_socket_ReceiveReady;

            ResponseSocket _response_socket1 = Context.CreateResponseSocket();
            _response_socket1.Connect(SERVER_ENDPOINT);//string.Format("tcp://{0}:{1}", address, _port1));
            _response_socket1.Options.Identity = Encoding.UTF8.GetBytes("S1");
            _response_socket1.ReceiveReady += _response_socket_ReceiveReady;

            ResponseSocket _response_socket2 = Context.CreateResponseSocket();
            _response_socket2.Connect(SERVER_ENDPOINT);//string.Format("tcp://{0}:{1}", address, _port1));
            _response_socket2.Options.Identity = Encoding.UTF8.GetBytes("S1");
            _response_socket2.ReceiveReady += _response_socket_ReceiveReady;
            //_response_socket.Poll();
            //while (true)
            //{
            //    var ret = _response_socket.ReceiveString();
            //    Console.WriteLine("Got " + ret);
            //    _response_socket.Send("I got u");
            //}
            Poller poller = new Poller();
            poller.AddSocket(_response_socket);
            poller.AddSocket(_response_socket1);
            poller.AddSocket(_response_socket2);
            poller.Start();
            //_response_socket.Poll();
            //while (true)
            //{
            //    bool hasMore = true;
            //    string msg = _response_socket.ReceiveString(out hasMore);
            //    if (string.IsNullOrEmpty(msg) || msg != "FIRST")
            //    {
            //        Console.WriteLine("No msg received.");
            //        break;
            //    }
            //    Console.WriteLine("Msg received! {0}", msg);
            //    Guid guid = PushSocket();
            //    _response_socket.Send(guid.ToString(), false, hasMore);

            //}
        }

        void _response_socket_ReceiveReady(object sender, NetMQSocketEventArgs e)
        {
            var b = e.Socket.Receive();
            var ret = Encoding.Unicode.GetString(b);
            var t = Encoding.UTF8.GetString(e.Socket.Options.Identity);
            if (!string.IsNullOrEmpty(ret))
            {
                Thread.Sleep(500);
                Console.WriteLine(t + "Got " + ret);
                e.Socket.Send(Encoding.Unicode.GetBytes(t + " Server got u:" + ret));
                return;
            }
            //if (ret == "Client")
            //{
            //    Console.WriteLine("Got " + ret);
            //    Guid guid = Guid.NewGuid();
            //    PushSocket(guid);
            //    Console.WriteLine("Push Dealer with " + guid);
            //    e.Socket.Send(guid.ToString());
            //}
        }

        public void Send(Guid guid, string msg)
        {
            _dealer_sockets[guid].Send(msg);
        }




        public void PushSocket(Guid guid)
        {
            var socket = NetMQContext.Create().CreateDealerSocket();
            _dealer_sockets.Add(guid, socket);

            socket.Options.Identity = Encoding.UTF8.GetBytes(guid.ToString());
            socket.Options.Linger = TimeSpan.Zero;
            socket.Connect(string.Format("tcp://{0}:{1}", _address, 6666));
            socket.ReceiveReady += socket_ReceiveReady;
        }

        void socket_ReceiveReady(object sender, NetMQSocketEventArgs e)
        {
            string msg = e.Socket.ReceiveString();
            if (string.IsNullOrEmpty(msg))
            {
                Console.WriteLine("Client send null");
                return;
            }
            Console.WriteLine("Got msg from {0}:{1}", Encoding.UTF8.GetString(e.Socket.Options.Identity), msg);
            e.Socket.Send("I got u");
        }
    }
}
