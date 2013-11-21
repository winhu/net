using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WinStudio.Permission.BusiServices.Interfaces;
using WinStudio.Permission.Common.Framework.Abstract;
using WinStudio.Permission.Principle;

namespace WinStudio.Permission.Web.Controllers
{
    public class PermissionApiController : AbstractPermissionMvcController
    {
        [HttpPost]
        public string GetUserPermission()
        {
            PermissionConfig config = HttpContext.GetPermissionConfig();
            if (!config.IsValidConfig) return string.Empty;
            var ip = GetIP();
            var ret = GetService<IPermissionBusiService>().GetUserPermission(config);
            //var ret = ibsPermission.GetUserPermission(sysauth, syscode, account);
            return ret.LastMsg;
        }
    }
}
