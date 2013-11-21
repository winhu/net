using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.BusiServices.Interfaces
{
    public interface ICustomerBusiService
    {
        ComRet SaveCustomer(Customer cust);
        ComRet GetCustomer(int mid, int id);
    }
}
