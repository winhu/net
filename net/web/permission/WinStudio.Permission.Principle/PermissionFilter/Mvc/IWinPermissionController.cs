using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    /// <summary>
    /// 如需权限需要跟业务代码挂钩，则必须继承该控制器
    /// </summary>
    public abstract class WinPermissionMvcController : Controller
    {
        public WinPermissionMvcController(string busicode)
        {
            HttpContext.SetWinPermissionBusiCode(busicode);
        }
    }
}
