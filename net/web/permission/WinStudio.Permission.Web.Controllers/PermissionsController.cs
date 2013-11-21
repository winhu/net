using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WinStudio.Permission.BusiServices.Interfaces;

namespace WinStudio.Permission.Web.Controllers
{
    public class PermissionsController : ApiController
    {
        ISysModuleBusiService servSubSystem;
        //[HttpPost]
        public string GetUserPermission()
        {
            string account = HttpContext.Current.Request.Form["Account"];
            string sysauth = HttpContext.Current.Request.Form["SysAuth"];
            string syscode = HttpContext.Current.Request.Form["SysCode"];
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(sysauth) || string.IsNullOrEmpty(syscode)) return string.Empty;
            servSubSystem = new SysModuleBusiService();
            var permission = servSubSystem.GetUserPermission(sysauth, syscode, account);
            return permission;
        }

    }
}
