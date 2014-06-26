using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Msmq.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RegisterClientService : RegisterClientIService
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void RegisterClient(ClientInfo clientInfo)
        {
            WcfServerPushServerBO.Instance.RegisterClient(clientInfo);
        }
    }
}
