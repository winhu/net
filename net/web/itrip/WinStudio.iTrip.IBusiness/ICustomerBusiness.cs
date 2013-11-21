using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.iTrip.Models;

namespace WinStudio.iTrip.IBusiness
{
    public interface ICustomerBusiness
    {
        ComRet RegisterCustomer(Customer customer);
    }
}
