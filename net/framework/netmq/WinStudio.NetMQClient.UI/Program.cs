using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.NetMQClient.UI
{
    class Program
    {
        public delegate void SendReadyEvent(string msg);
        static void Main(string[] args)
        {
            Console.WriteLine("I am client, press Tab to stop");
            //Demo demo = new Demo();
            //demo.StartClient();

            var line = "quit";
            Client client = new Client();
            client.OnReceiveReady += new Client.ReceiveReadyEvent(Received);
            //SendEvent += client.Send;
            while ((line = Console.ReadLine()) != "quit")
            {
                //Send(line);
                client.Send(line);
                line = "";
            }
            Console.ReadKey();
        }

        static void Received(string msg)
        {
            Console.WriteLine("Client got msg:" + msg);
        }
        static SendReadyEvent SendEvent;
        static void Send(string msg)
        {
            if (SendEvent != null)
                SendEvent(msg);
        }
    }
}
