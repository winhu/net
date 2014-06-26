using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Msmq.Client;

namespace WinStudio.Msmq.Server
{
    public interface MessageQueueManager
    {
        void RegisterMessageQueue(string queueName, MessageFormatterType type, params Type[] types);

        void RegisterClient(IClientInfo clientinfo);
    }
}
