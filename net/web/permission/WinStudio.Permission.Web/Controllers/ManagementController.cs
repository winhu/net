using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WinStudio.Permission.BusiServices.Interfaces;
using WinStudio.Permission.Common.Framework.Abstract;
using WinStudio.Permission.Common.Framework.Attributes;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.Web.Controllers
{
    [NeedPermissionAdmin]
    public class ManagementController : AbstractPermissionMvcController
    {
        ISysModuleBusiService ibsSubSystem;
        IRoleBusiService ibsRole;
        IFunctionBusiService ibsFunction;
        ICustomerBusiService ibsCustomer;
        public ManagementController(
            ISysModuleBusiService bsSubSystem,
            IRoleBusiService bsRole,
            IFunctionBusiService bsFunction,
        ICustomerBusiService bsCustomer)
        {
            ibsSubSystem = bsSubSystem;
            ibsRole = bsRole;
            ibsFunction = bsFunction;
            ibsCustomer = bsCustomer;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SysModule(int id = 0)
        {
            return View();
        }
        [HttpPost]
        public ActionResult SysModuleEdit(SysModule module)
        {
            return View();
        }
        public ActionResult Role(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult RoleEdit(Role role)
        {
            return View();
        }
        public ActionResult Function(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult FunctionEdit(Function function)
        {
            return View();
        }
        public ActionResult Customer(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustomerEdit(Customer customer)
        {
            return View();
        }
    }
}
