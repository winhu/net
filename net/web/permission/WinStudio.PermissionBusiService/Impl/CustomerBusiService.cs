using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Permission.Models;

namespace WinStudio.PermissionBusiService
{
    public class CustomerBusiService : BaseBusiService<Customer>
    {
        public void SaveCustomer(Customer cust, int sysid)
        {
            Save(cust);
            if (sysid > 0)
            {
                SubSystemBusiService systemServ = new SubSystemBusiService(this.Get<PermissionDbContext>());
                SysModule system = systemServ.GetById(sysid);
                if (system == null) return;
                system.Customers.Add(cust);
                systemServ.Save(system);
            }
        }
    }
}
