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
using WinStudio.Permission.Common.Framework.Attributes;
using WinStudio.Permission.Models;
using WinStudio.Permission.Principle;

namespace WinStudio.Permission.Web.Controllers
{
    [NeedPermissionAdmin]
    public class PermissionController : AbstractPermissionMvcController
    {
        //
        // GET: /Permission/

        PermissionTreeBusiService perService = new PermissionTreeBusiService();
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public string GetUserPermission()
        //{
        //    PermissionConfig config = HttpContext.GetPermissionConfig();
        //    if (!config.IsValidConfig) return string.Empty;
        //    var ip = GetIP();
        //    var ret = GetService<IPermissionBusiService>().GetUserPermission(config);
        //    //var ret = ibsPermission.GetUserPermission(sysauth, syscode, account);
        //    return ret.LastMsg;
        //}

        public ActionResult GetPermissionRoot(bool editable = true)
        {
            return Json(perService.GetPermissionTreeRoot(editable));
        }
        public ActionResult GetPermissionNodes(int id = 0, int rpid = 0, int type = 0, int ptype = 0, int stype = 1)
        {
            return Json(perService.GetPermissionNodes(id, rpid, type, ptype, stype));
        }


    }
}
