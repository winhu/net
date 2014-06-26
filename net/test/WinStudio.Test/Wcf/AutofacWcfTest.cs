using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using WinStudio.WcfAutofac;
using WinStudio.WcfAutofac.Services;

namespace WinStudio.Test.Wcf
{
    class AutofacWcfTest
    {
        public void WcfAutofacClientTest()
        {
            //WcfClient.Instance.SetHost("http://localhost:30756");
            //var serv = WcfClient.Instance.GetService<IServiceCustomer>("ServiceCustomer");
            //var msg = serv.DoWork("hyf");
            //Console.WriteLine(msg);

            var builder = new ContainerBuilder();


            builder.Register(c => new ChannelFactory<IServiceFactory>(
                new BasicHttpBinding(),
                new EndpointAddress("http://localhost:30756/ServiceCustomer")))
              .SingleInstance();

            builder.Register(c => c.Resolve<ChannelFactory<IServiceCustomer>>().CreateChannel())
              .UseWcfSafeRelease();

            //builder.Register(c => c.Resolve<ChannelFactory<IServiceFactory>>().CreateChannel())
            //  .UseWcfSafeRelease();

            // Another application class not using WCF
            //builder.RegisterType<AlbumPrinter>();

            var container = builder.Build();

            var customer = container.Resolve<IServiceCustomer>();
            var ret = customer.DoWork("hyf");
            Console.WriteLine(ret);

        }
    }
}
