using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinStudio.Permission.Common.Framework.Abstract;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.Web.Controllers
{
    public class HomeController : AbstractPermissionMvcController
    {
        public ActionResult Index()
        {
            //if (null == HttpContext.Application[MvcApplication.PermissionSysConfig])
            //    return RedirectToAction("About", new { reason = "系统未启动。", opentime = DateTime.Now.AddHours(3) });

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";


            return View();
        }

        public ActionResult About(string reason, DateTime opentime)
        {
            ViewBag.Message = string.IsNullOrEmpty(reason) ? "系统暂时停止服务。" : reason;
            ViewBag.OpenTime = opentime.ToString("yyyy-MM-dd HH:MM");

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
