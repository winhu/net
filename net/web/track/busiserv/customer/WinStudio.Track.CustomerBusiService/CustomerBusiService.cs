using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Track.Framework.Core;
using WinStudio.Track.IBusiService;
using WinStudio.Track.Models.Tracking;

namespace WinStudio.Track.CustomerBusiService
{
    [Export(typeof(ICustomerBusiService))]
    public class CustomerBusiService : AbstractTrackBusiService<Customer>, ICustomerBusiService
    {
        public ComRet SyncCustomer(string account)
        {
            throw new NotImplementedException();
        }

        public ComRet Register(Customer customer)
        {

            Save(customer);
            return Result(true);
        }

        public ComRet NeedRegister(string account)
        {
            throw new NotImplementedException();
        }
    }
}
