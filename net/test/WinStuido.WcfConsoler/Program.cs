using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WinStudio.WcfAutofac.Services;
using WinStudio.WcfManager;

namespace WinStuido.WcfConsoler
{
    class Program
    {
        static void Main(string[] args)
        {
            HostInfo host = new HostInfo("127.0.0.1:9998").LoadTypesFromAssemblies(WinAssemblyUtility.GetAssembly());
            using (ServiceContainer container = new ServiceContainer())
            {
                container.OnServiceStarted += OnOpened;
                container.Open(host);
                Console.WriteLine("press any key to quit");
                IServiceCustomer cust = new HostInfo("127.0.0.1:9999").GetService<IServiceCustomer>();
                var ret = cust.DoWork("hyf");
                Console.WriteLine(ret);
                //IServiceAuthenticationReception serv = new HostInfo("127.0.0.1:9999").GetService<IServiceAuthenticationReception>();
                //var ret = serv.Register("hyf", "888");// LoadFactory("hyf");
                //Console.WriteLine(ret.Ret);
                //Console.WriteLine(ret.Msg);
                Console.ReadKey();
                container.Close(host);
                Console.ReadKey();
                return;
            }
        }

        public static void OnOpened(object sender, EventArgs e)
        {
            Console.WriteLine(sender + " open");
        }
    }
}
