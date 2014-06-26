using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Msmq.Server
{

    public class QueueItem
    {
        #region Members

        public enum EQueueState
        {
            eQueueStatePending,		// Pending to Notify
            eQueueStateSending,		// Sending
            eQueueStateIdle		// Idle Mode
        }

        string groupName = "";
        string clientName = "";
        string pushMessageBoxAddress = "";
        EQueueState queueState = EQueueState.eQueueStateIdle;

        #endregion

        public QueueItem(string groupName, string clientName, string pushMessageBoxAddress)
        {
            Init(groupName, clientName, pushMessageBoxAddress, EQueueState.eQueueStateIdle);
        }

        public QueueItem(QueueItem QueueItem)
        {
            Init(QueueItem.GroupName, QueueItem.ClientName, QueueItem.PushMessageBoxAddress, QueueItem.QueueState);
        }

        protected void Init(string groupName, string clientName, string pushMessageBoxAddress, EQueueState queueState)
        {
            this.GroupName = groupName;
            this.ClientName = clientName;
            this.PushMessageBoxAddress = pushMessageBoxAddress;
            this.QueueState = queueState;
        }

        public string GroupName
        {
            get { return groupName; }
            set
            {
                if (groupName != value)
                {
                    groupName = value;
                    OnPropertyChanged("QueueItem.GroupName.Changed");
                }
            }
        }

        public string ClientName
        {
            get { return clientName; }
            set
            {
                if (clientName != value)
                {
                    clientName = value;
                    OnPropertyChanged("QueueItem.ClientName.Changed");
                }
            }
        }

        public string PushMessageBoxAddress
        {
            get { return pushMessageBoxAddress; }
            set
            {
                if (pushMessageBoxAddress != value)
                {
                    pushMessageBoxAddress = value;
                    OnPropertyChanged("QueueItem.PushMessageBoxAddress.Changed");
                }
            }
        }

        public EQueueState QueueState
        {
            get { return queueState; }
            set
            {
                if (queueState != value)
                {
                    queueState = value;
                    OnPropertyChanged("QueueItem.QueueState.Changed");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
