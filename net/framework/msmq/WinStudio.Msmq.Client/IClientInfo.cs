using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace WinStudio.Msmq.Client
{
    public enum ClientType
    {
        User = 0,
        Server = 1
    }
    public enum GroupType
    {
        User = 0,
        Server = 1
    }
    public interface IClientInfo<T>// where T : new()
    {
        string ClientName { get; }

        void RegisterClient(string msmqServerName);

        string Send(T package);

        void BeginReceive();

        void Close();
    }

    public delegate void HandlMessageEvent(object message);
    public abstract class ClientInfo<T> : IClientInfo<T>//where T : new()
    {
        public ClientInfo(string name)
        {
            _clientName = name;
        }

        private string _clientName;
        private MessageQueue queue;
        public string ClientName
        {
            get { return _clientName; }
        }

        public void Close()
        {
            if (queue != null)
            {
                queue.Close();
                queue = null;
            }
        }

        public void RegisterClient(string msmqServerName)
        {
            //string path = string.Format("FormatName:Direct=TCP:{0}\\private$\\{1}", msmqServerName, _clientName);
            string path = string.Format(@"{0}\private$\{1}", msmqServerName, _clientName);
            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path);
            }
            queue = new MessageQueue(path);
            if (queue == null) throw new Exception("queue未找到");
            queue.SetPermissions("EveryOne", MessageQueueAccessRights.FullControl);
            queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
        }

        public HandlMessageEvent HandlMessage;

        void queue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            if (sender != null)
            {
                MessageQueue mq = sender as MessageQueue;
                Message message = mq.EndReceive(e.AsyncResult);
                if (HandlMessage != null)
                {
                    HandlMessage(message.Body);
                }
                mq.BeginReceive();
            }
        }

        public string Send(T package)
        {
            if (queue == null) return string.Empty;
            Message message = new Message();
            message.Body = package;
            message.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            queue.Send(message);
            return message.Id;
        }


        public void BeginReceive()
        {
            if (HandlMessage != null)
            {
                queue.ReceiveCompleted += queue_ReceiveCompleted;
                queue.BeginReceive();
            }
        }
    }

    public interface ITester
    {
        string Name { get; set; }

        string Show();
    }

    [Serializable]
    public class Tester : ITester
    {
        public string Name { get; set; }

        public string Show()
        {
            return "I am " + Name;
        }
    }

    public class demo
    {

    }
}
