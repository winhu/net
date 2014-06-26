using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;

namespace WinStudio.NetMQServer
{
    public class Factory
    {

        private static Factory _factory = new Factory();
        public static Factory Instance { get { return _factory; } }

        private NetMQContext _ctx = NetMQContext.Create();
        public NetMQSocket Create(string address, int port, string flag)
        {
            var server = _ctx.CreateResponseSocket();
            server.Options.Identity = Encoding.Unicode.GetBytes(flag);
            server.Connect(string.Format("tcp://{0}:{1}", address, port));
            return server;
        }
    }
}
