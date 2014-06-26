using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WinStudio.WcfAutofacDemo
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IServiceCustomer”。

    public class ServiceCustomer : IServiceCustomer
    {
        public string DoWork(string account)
        {
            return string.Format("hello {0}", account);
        }
    }
}
