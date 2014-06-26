using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace WinStudio.Test
{
    public interface ITester
    {
        Type MyType { get; }
    }
    public class Tester : ITester
    {
        public Type MyType
        {
            get { return this.GetType(); }
        }
    }
    public class TesterA : Tester
    {
    }
    public class TesterB : Tester
    {
    }

    public class Class1
    {
        List<ITester> testers = new List<ITester>() { 
            new TesterA(), new TesterB(), new TesterB(), new TesterA()
        };
        public void run()
        {
            ServiceCustomer.ServiceCustomerClient client = new ServiceCustomer.ServiceCustomerClient();
            var ret = client.DoWork("hyf");
            Console.WriteLine(ret);

            List<object> objs = new List<object>();
            objs.Add(new TesterA());
            objs.Add(new TesterB());
            objs.Add(new TesterB());
            objs.Add(new TesterA());

            objs.ForEach(o => Console.WriteLine(o.GetType().IsSameOrInherited<ITester>()));

            testers.ForEach(t => Console.WriteLine(t.MyType.FullName));
        }
    }
}
