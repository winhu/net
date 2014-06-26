using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;

namespace WinStudio.WcfAutofac
{
    public class WcfClient
    {
        private static WcfClient _instance = new WcfClient();
        public static WcfClient Instance
        {
            get
            {
                return _instance;
            }
        }

        private string _host;
        protected string HostAddr
        {
            get
            {
                return _host;
            }
        }
        public void SetHost(string host)
        {
            _host = host;
        }

        protected string GetServerAddr(string serviceName)
        {
            return string.Format("{0}/{1}", HostAddr, serviceName);
        }

        public T GetService<T>(string serviceName)
        {
            var builder = new ContainerBuilder();


            builder.Register(c => new ChannelFactory<T>(
                new BasicHttpBinding(),
                new EndpointAddress(GetServerAddr(serviceName))))
              .SingleInstance();

            builder.Register(c => c.Resolve<ChannelFactory<T>>().CreateChannel())
              .UseWcfSafeRelease();



            // Another application class not using WCF
            //builder.RegisterType<AlbumPrinter>();

            var container = builder.Build();

            T t = container.Resolve<T>();
            return t;
        }
    }
}
