using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WinStuido.WcfConsoler
{
    public class WinHost<T> : ServiceHost
    {
        public WinHost() : base(typeof(T)) { }
        public string Name { get { return typeof(T).FullName; } }
    }
}
