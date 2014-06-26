using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WinStuido.Permission.IBusiness.Reception
{
    public static class ExtensionUtility
    {
        public static string GetSecurityKey(this HttpContextBase context)
        {
            return context.GetPermissionCookie(WinWebGlobalManager.Config.WinTokenName);
        }

    }
}
