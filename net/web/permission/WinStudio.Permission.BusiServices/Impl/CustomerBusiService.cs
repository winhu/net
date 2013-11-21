using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Permission.BusiServices.Interfaces;
using WinStudio.Permission.Common.Framework.Abstract;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.BusiServices.Impl
{
    [Export(typeof(ICustomerBusiService))]
    public class CustomerBusiService : AbstractPermissionBusiService<Customer>, ICustomerBusiService
    {
        public ComRet SaveCustomer(Customer cust)
        {
            if (cust.IsNewModel() && Exists(cust)) return Result("该账号或用户名已经存在！");
            if (cust.ModuleID <= 0 && !Save(cust))
                return Result("保存用户失败！");
            if (cust.ModuleID > 0)
            {
                //SysModuleBusiService systemServ = new SysModuleBusiService(this.Get<PermissionDbContext>());
                ISysModuleBusiService ibsSysModule = GetService<ISysModuleBusiService>();
                //ibsSysModule.SetContext(this.Get());
                var ret = ibsSysModule.GetSysModule(cust.ModuleID);
                if (!ret.Ret)
                    return ret;
                var system = ret.Instance<SysModule>();
                system.Customers.Add(cust);
                ibsSysModule.SaveSysModule(system);
            }
            return Result(true, "操作成功！");
        }

        public bool Exists(Customer cust)
        {
            return Exist(c => c.Account == cust.Account || c.Name == cust.Name);
        }

        public ComRet GetCustomer(int mid, int id)
        {
            if (id <= 0) return Result(new Customer(mid));
            return Result(GetById(id).SetModule(mid));
        }
    }
}
