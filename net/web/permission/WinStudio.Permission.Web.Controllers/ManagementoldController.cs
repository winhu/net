using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WinStudio.ComResult;
using WinStudio.Permission.BusiServices.Impl;
using WinStudio.Permission.BusiServices.Interfaces;
using WinStudio.Permission.Common.Framework.Abstract;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.Web.Controllers
{
    public class ManagementoldController : AbstractPermissionMvcController
    {
        ISysModuleBusiService servSubSystem;
        IRoleBusiService servRole;
        IFunctionBusiService servFunction;
        ICustomerBusiService servCustomer;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Editor(int id, PermissionNodeType type)
        {
            return View();
        }

        public ActionResult RegSystemList()
        {
            servSubSystem = new SysModuleBusiService();
            return View(servSubSystem.GetAllUsingSysModules().Instance());
        }
        public ActionResult RegSystemDetail(int sysid)
        {
            servSubSystem = new SysModuleBusiService();
            return View(servSubSystem.GetSysModule(sysid).Instance<SysModule>());
        }
        public ActionResult EditorRegSystem(int id = 0)
        {
            servSubSystem = new SysModuleBusiService();
            return View(servSubSystem.GetSysModule(id).Instance<SysModule>());
        }
        public ActionResult RegSystemFunctionList(int sysid)
        {
            ViewBag.SystemID = sysid;
            servSubSystem = new SysModuleBusiService();
            return View(servSubSystem.GetSysModule(sysid).Instance<SysModule>().Functions);
        }

        public ActionResult RegSystemRoleList(int sysid)
        {
            ViewBag.SystemID = sysid;
            servSubSystem = new SysModuleBusiService();
            return View(servSubSystem.GetSysModule(sysid).Instance<SysModule>().Roles);
        }
        public ActionResult RegSystemCustomerList(int sysid)
        {
            ViewBag.SystemID = sysid;
            servSubSystem = new SysModuleBusiService();
            return View(servSubSystem.GetSysModule(sysid).Instance<SysModule>().Customers);
        }

        public ActionResult EditorRole(int id = 0)
        {
            return View();
        }
        public ActionResult EditorFunction(int id = 0)
        {
            return View();
        }
        public ActionResult EditorCustomer(int id = 0, int sysid = 0)
        {
            ViewBag.SystemID = sysid;
            servCustomer = new CustomerBusiService();
            return View(servCustomer.GetCustomer(id).Instance<Customer>());
        }

        [HttpPost]
        public ActionResult EditorRegSystem(SysModule entity)
        {
            servSubSystem = new SysModuleBusiService();
            servSubSystem.SaveSysModule(entity);
            return View();
        }
        [HttpPost]
        public ActionResult EditorRole(Role entity)
        {
            servRole = new RoleBusiService();
            servRole.SaveRole(entity);
            return View();
        }
        [HttpPost]
        public ActionResult EditorFunction(Function entity)
        {
            servFunction = new FunctionBusiService();
            servFunction.SaveFunction(entity);
            return View();
        }
        [HttpPost]
        public ActionResult EditorCustomer(Customer entity)
        {
            servCustomer = new CustomerBusiService();
            int sysid = HttpContext.Request.Form["SystemID"].ToInt(0);
            servCustomer.SaveCustomer(entity, sysid);
            return View();
        }

        public ActionResult AutoInit()
        {
            servSubSystem = new SysModuleBusiService();
            var systems = servSubSystem.GetAllUsingSysModules().Instance() as Module[];
            ViewBag.SubSystems = new SelectList(systems, "Code", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult PermissionAutoInitializer()
        {
            var SubSystem = HttpContext.Request.Form["SubSystem"];
            var FunctionArea = HttpContext.Request.Form["FunctionArea"];
            if (string.IsNullOrEmpty(SubSystem))
                return View("请选择系统！");
            if (string.IsNullOrEmpty(FunctionArea))
                return View("请设定域名称！");
            if (HttpContext.Request.Files.Count == 0)
                return View("请选择权限文件！");
            HttpPostedFileBase file = HttpContext.Request.Files[0];
            var dir = Server.MapPath(string.Format("Files/{0}/{1}/{2}", SubSystem, FunctionArea, DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            var path = Path.Combine(dir, file.FileName);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            HttpContext.Request.Files[0].SaveAs(path);
            servSubSystem = new SysModuleBusiService();
            var ret = servSubSystem.AutoGeneratePermission(path, SubSystem, FunctionArea);
            return View();
        }

        public ActionResult List()
        {
            return View();
        }
    }
}
