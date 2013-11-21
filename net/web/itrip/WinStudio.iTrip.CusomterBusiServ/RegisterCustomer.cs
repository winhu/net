using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Repository;
using WinStudio.ComResult;
using WinStudio.iTrip.Core.AbstractServices;
using WinStudio.iTrip.IBusiness;
using WinStudio.iTrip.Models;

namespace WinStudio.iTrip.CusomterBusiServ
{

    [Export(typeof(ICustomerBusiness))]
    public class RegisterBusiServ : iTripBusiServ, ICustomerBusiness
    {
        public ComRet RegisterCustomer(Customer customer)
        {
            if (customer.IsValid())
            {
                if (MongoEntity.Exists<Customer>(c => c.Account == customer.Account))
                {
                    return Result("账户已经存在");
                }
                customer.Save();
                return Result();
            }
            return Result(customer.GetMsg());
        }
    }
}
