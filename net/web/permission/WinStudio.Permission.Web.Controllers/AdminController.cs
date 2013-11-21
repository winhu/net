using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinStudio.Framework.Authentication;
using WinStudio.Permission.BusiServices.Impl;
using WinStudio.Permission.Common.Framework;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.Web.Controllers
{
    [NeedPermission("管理员入口")]
    public class AdminController : Controller
    {
        [NeedPermission("权限管理首页")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [NeedPermission("权限管理首页", Display = false)]
        public ActionResult Index(SysConfig config)
        {
            if (!AdminBusiService.IsDatabaseExist(config))
                AdminBusiService.CreateDatabase();
            HttpContext.Application[WebConsts.WinWebApplicationConfigFlag] = config;
            return View();
        }
    }
}