using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.WcfAutofac.Services
{
    [ServiceContract]
    public interface IServiceFactory
    {
        [OperationContract]
        Result LoadFactory(string name);
    }
    [WinWcfService]
    public class ServiceFactory : IServiceFactory
    {
        public Result LoadFactory(string name)
        {
            return new Result() { Ret = true, Msg = string.Format("Factory {0} was opened at {1} ", name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) };
        }
    }

    [ServiceContract]
    public abstract class ResultBase
    {
        [DataMember]
        public bool Ret { get; set; }

        [DataMember]
        public string Msg { get; set; }

        [DataMember]
        public int Num { get; set; }

        [DataMember]
        public object Obj { get; set; }

        public string GetMsg() { return Msg; }
    }

    public class Result : ResultBase { }

}
