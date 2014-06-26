using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace WinStudio.NetMQClient
{
    public class Client
    {
        private static int REQUEST_TIMEOUT = 1500;
        private static int REQUEST_RETRIES = 1110;
        private static string SERVER_ENDPOINT = "tcp://127.0.0.1:5555";

        private static string _strSequenceSent = "";
        private static bool _expectReply = true;
        private static int _retriesLeft = 0;
        private static string _address = "127.0.0.1";
        private static int _port = 5555;

        public Client()
        {
            //init();
        }
        public Client(string address, int port)
        {
            _address = address;
            _port = port;
            //init();
        }

        public static string EndPoint
        {
            get
            {
                return string.Format("tcp://{0}:{1}", _address, _port);
            }
        }


        //private Poller _poller = new Poller();
        private RequestSocket _socket;
        private NetMQSocket init()
        {
            if (_socket == null)
            {
                _socket = CreateServerSocket(NetMQContext.Create());
            }
            return _socket;
        }

        void _socket_SendReady(object sender, NetMQSocketEventArgs e)
        {
            if (_queue.Count > 0)
            {
                string msg = _queue.Dequeue();
                e.Socket.Send(msg);
            }
        }

        public delegate void ReceiveReadyEvent(string msg);

        public ReceiveReadyEvent OnReceiveReady;
        //public SendReadyEvent OnSendReady;

        void _socket_ReceiveReady(object sender, NetMQSocketEventArgs e)
        {
            string msg = e.Socket.ReceiveString();
            Console.WriteLine("From Server:" + msg);
            if (OnReceiveReady != null)
                OnReceiveReady(msg);

            //if (_queue.Count == 0)
            //    _poller.Stop();

        }

        private Queue<string> _queue = new Queue<string>();
        private List<string> _msg = new List<string>();

        public void Send(string msg)
        {
            _retriesLeft = REQUEST_RETRIES;

            //using (NetMQContext context = NetMQContext.Create())
            //{
            var client = init();// CreateServerSocket(context);

            int sequence = 0;
            bool result = true;
            while (sequence < 10)
            {
                sequence++;
                _strSequenceSent = msg + " " + sequence + " HELLO";
                Console.WriteLine("C: Sending ({0})", _strSequenceSent);
                client.Send(Encoding.Unicode.GetBytes(_strSequenceSent));
                _expectReply = true;

                //while (_expectReply)
                //{
                result = client.Poll(TimeSpan.FromMilliseconds(REQUEST_TIMEOUT));
            }
            //client.Disconnect(SERVER_ENDPOINT);
            //client.Close();
            //client.Dispose();
            //Console.WriteLine(result);
            //    if (!result)
            //    {
            //        _retriesLeft--;

            //        if (_retriesLeft == 0)
            //        {
            //            Console.WriteLine("C: Server seems to be offline, abandoning");
            //            break;
            //        }
            //        else
            //        {
            //            Console.WriteLine("C: No response from server, retrying..");

            //            client.Disconnect(SERVER_ENDPOINT);
            //            client.Dispose();
            //            client = null;
            //            client = CreateServerSocket(context);
            //            client.Send(Encoding.Unicode.GetBytes(_strSequenceSent));
            //        }
            //    }
            //}
            //}
        }

        private RequestSocket CreateServerSocket(NetMQContext context)
        {
            Console.WriteLine("C: Connecting to server...");

            var client = context.CreateRequestSocket();
            client.Options.Linger = TimeSpan.Zero;
            Guid guid = Guid.NewGuid();
            client.Options.Identity = Encoding.Unicode.GetBytes(guid.ToString());
            client.Connect(SERVER_ENDPOINT);
            client.ReceiveReady += _socket_ReceiveReady;// ClientOnReceiveReady;

            return client;
        }

        private void ClientOnReceiveReady(object sender, NetMQSocketEventArgs socket)
        {
            var reply = socket.Socket.Receive();

            if (Encoding.Unicode.GetString(reply) == (_strSequenceSent + " WORLD!"))
            {
                Console.WriteLine("C: Server replied OK ({0})", Encoding.Unicode.GetString(reply));
                _retriesLeft = REQUEST_RETRIES;
                _expectReply = false;
            }
            else
            {
                Console.WriteLine("C: Malformed reply from server: {0}", Encoding.Unicode.GetString(reply));
            }
        }
    }
}
