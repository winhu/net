using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WinStudio.ComResult;
using WinStudio.Permission.BusiServices.Interfaces;
using WinStudio.Permission.Common.Framework.Abstract;
using WinStudio.Permission.Common.Framework.Attributes;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.Web.Controllers
{
    [NeedPermissionAdmin]
    public class ManagementController : AbstractPermissionMvcController
    {
        ISysModuleBusiService ibsSysModule;
        IRoleBusiService ibsRole;
        IFunctionBusiService ibsFunction;
        ICustomerBusiService ibsCustomer;
        IRFunctionBusiService ibsRFunction;
        public ManagementController(
            ISysModuleBusiService bsSubSystem,
            IRoleBusiService bsRole,
            IFunctionBusiService bsFunction,
        ICustomerBusiService bsCustomer)
        {
            ibsSysModule = bsSubSystem;
            ibsRole = bsRole;
            ibsFunction = bsFunction;
            ibsCustomer = bsCustomer;
        }

        public ActionResult Index()
        {
            var id = HttpContext.GetParam("id").ToInt(0);
            var rpid = HttpContext.GetParam("rpid").ToInt(0);
            var type = HttpContext.GetParam("type").ToInt(0);
            var ptype = HttpContext.GetParam("ptype").ToInt(0);
            var stype = HttpContext.GetParam("stype").ToInt(0);

            if (type == (int)PermissionNodeType.Virtual &&
                ptype == (int)PermissionNodeType.Virtual)
            {
                return RedirectToAction("SysModuleEdit", new { id = 0 });
            }
            if (type == (int)PermissionNodeType.Virtual &&
                stype == (int)PermissionNodeType.Role &&
                ptype == (int)PermissionNodeType.Module)
            {
                return RedirectToAction("RoleEdit", new { id = 0, mid = rpid, pid = 0 });
            }
            if (type == (int)PermissionNodeType.Virtual &&
                stype == (int)PermissionNodeType.Role &&
                ptype == (int)PermissionNodeType.Role)
            {
                return RedirectToAction("RoleEdit", new { id = 0, mid = 0, pid = rpid });
            }

            if (type == (int)PermissionNodeType.Virtual &&
                stype == (int)PermissionNodeType.Customer)
            {
                return RedirectToAction("CustomerEdit", new { id = 0, mid = rpid });
            }

            if (type == (int)PermissionNodeType.Module)
            {
                return RedirectToAction("SysModuleEdit", new { id = id });
            }
            if (type == (int)PermissionNodeType.Role)
                return RedirectToAction("RoleEdit", new { id = id, mid = 0 });
            if (type == (int)PermissionNodeType.Customer)
                return RedirectToAction("CustomerEdit", new { id = id });
            if (type == (int)PermissionNodeType.Customer)
                return RedirectToAction("FunctionEdit", new { id = id });

            return View();
        }

        public ActionResult SysModule(int id = 0)
        {
            var ret = ibsSysModule.GetAllUsingSysModules();
            if (ret.Ret)
                return View(ret.Instance<List<SysModule>>());
            return View(new List<SysModule>());
        }

        public ActionResult SysModuleEdit(int id = 0)
        {
            if (id == 0) return View();
            var ret = ibsSysModule.GetSysModule(id);
            if (ret.Ret) return View(ret.Instance<SysModule>());
            return View();
        }
        [HttpPost]
        public ActionResult SysModuleEdit(SysModule module)
        {
            if (ModelState.IsValid && module.IsValid())
            {
                var ret = ibsSysModule.SaveSysModule(module);
                if (ret.Ret)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("", "操作成功！");
                    //return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", ret.LastMsg);
            }
            else
            {
                ModelState.AddModelError("", module.ValidMsg);
            }
            return View(module);
        }
        public ActionResult Role(int id = 0)
        {
            ViewBag.ModuleID = id;
            var ret = ibsRole.GetRoles(id);
            if (ret.Ret)
                return View(ret.Instance<List<Role>>());
            return View(new List<Role>());
        }
        public ActionResult RoleEdit(int mid, int pid = 0, int id = 0)
        {
            var ret = ibsRole.GetRole(mid, pid, id);
            return ViewComRet<Role>(ret);
        }
        [HttpPost]
        public ActionResult RoleEdit(Role role)
        {
            if (ModelState.IsValid && role.IsValid())
            {
                var ret = ibsRole.SaveRole(role);
                if (ret.Ret)
                    ModelState.AddModelError("", "操作成功！");
                else
                    ModelState.AddModelError("", ret.LastMsg);
            }
            else
            {
                ModelState.AddModelError("", role.ValidMsg);
            }
            return View(role);
        }
        public ActionResult Function(int id)
        {
            var ret = ibsSysModule.GetSysModule(id);
            if (ret.Ret)
                return View(ret.Instance<SysModule>().Functions);
            return View(new List<Function>());
        }
        public ActionResult FunctionEdit(int id)
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
            ViewBag.ModuleID = id;
            var ret = ibsSysModule.GetSysModule(id);
            if (ret.Ret)
                return View(ret.Instance<SysModule>().Customers);
            return View(new List<Customer>());
        }

        public ActionResult CustomerEdit(int id = 0, int mid = 0)
        {
            var ret = ibsCustomer.GetCustomer(mid, id);
            return View(ret.Instance<Customer>());
        }
        [HttpPost]
        public ActionResult CustomerEdit(Customer customer)
        {
            if (ModelState.IsValid && customer.IsValid())
            {
                var ret = ibsCustomer.SaveCustomer(customer);
                if (ret.Ret)
                    ModelState.AddModelError("", "操作成功！");
                else
                    ModelState.AddModelError("", ret.LastMsg);
            }
            else
            {
                ModelState.AddModelError("", customer.ValidMsg);
            }
            return View(customer);
        }

        public ActionResult AutoInit(int id = 0, string msg = null)
        {
            var ret = ibsSysModule.GetSysModule(id);
            if (!string.IsNullOrEmpty(msg))
                ModelState.AddModelError("", Server.UrlDecode(msg));
            return View(ret.Instance<SysModule>());
        }
        [HttpPost]
        public ActionResult DoAutoInit()
        {
            var mid = HttpContext.GetParam("ID").ToInt(0);
            var FunctionArea = HttpContext.GetParam("Code");
            try
            {
                if (!ibsSysModule.Exists(mid).Ret)
                    return View("请选择系统！");
                if (string.IsNullOrEmpty(FunctionArea))
                    return View("请设定域名称！");
                if (HttpContext.Request.Files.Count == 0)
                    return View("请选择权限文件！");
                HttpPostedFileBase file = HttpContext.Request.Files[0];
                var dir = Server.MapPath(string.Format("Files/{0}/{1}/{2}", mid, string.IsNullOrEmpty(FunctionArea) ? mid.ToString() : FunctionArea, DateTime.Now.ToString("yyyyMMddHHmmssfff")));
                var path = Path.Combine(dir, file.FileName);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                HttpContext.Request.Files[0].SaveAs(path);
                ibsSysModule = GetService<ISysModuleBusiService>();
                ComRet ret = ibsSysModule.AutoGeneratePermission(path, mid, FunctionArea);
                return RedirectToAction("AutoInit", new { id = mid, msg = "操作成功！" });
            }
            catch (Exception e)
            {
                return RedirectToAction("AutoInit", new { id = mid, msg = "操作失败！" });
            }
        }

        public ActionResult CopyFunctions(int mid, int rid, string funcs)
        {
            ibsSysModule = GetService<ISysModuleBusiService>();
            var ret = ibsSysModule.GetSysModule(mid);
            if (!ret.Ret)
                return JsonMsg(false, "您选择的系统模块不存在！");
            ibsRole = GetService<IRoleBusiService>();
            ret = ibsRole.CopyFunctions(rid, funcs, ret.Instance<SysModule>().Functions);
            return JsonMsg(ret);
        }

        public ActionResult DeleteRFunction(int id)
        {
            ibsRFunction = GetService<IRFunctionBusiService>();
            return JsonMsg(ibsRFunction.DeleteFunction(id));
        }
    }
}
