using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Track.Models.Tracking;

namespace WinStudio.Track.IBusiService
{
    public interface ICustomerBusiService
    {
        ComRet SyncCustomer(string account);
        ComRet Register(Customer customer);
        ComRet NeedRegister(string account);
    }
}
