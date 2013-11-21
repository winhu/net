using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Framework.Authentication
{
    public class WebConsts
    {
        public const string WinWebApplicationConfigFlag = "Win_Web_Application_Config_Flag";
        public static string WinTokenName = "WinToken";
        public static string WinTokenDomain = "";
        public static int WinTokenTimeout = 20;
        public static string WinHandlePermissionAddress = "http://localhost/Permission/GetUserPermission";
        public static string WinHandleProtalAddress = "http://localhost";
        public static string WinHandleLoginAddress = "http://localhost/Account/Login";
        public static string WinHandleLogoutAddress = "http://localhost/Account/Logout";
        public static string WinHandleValidatationAddress = "http://localhost/Account/Validation";
        public static string WinHandleUpdateInfoAddress = "http://localhost/Account/UpdateInfo";

        public static string WinTmpException = "WinExceptionTempData";
    }

    public class WinWebGlobalConfiguration
    {
        public static void Initialize(bool AuthenticationServer = false)
        {
            WebConsts.WinTokenName = ConfigurationManager.AppSettings["WinTokenName"].ToString();
            WebConsts.WinTokenDomain = ConfigurationManager.AppSettings["WinTokenDomain"].ToString();
            WebConsts.WinTokenTimeout = ConfigurationManager.AppSettings["WinTokenTimeout"].ToInt(20);

            WebConsts.WinHandlePermissionAddress = ConfigurationManager.AppSettings["WinHandlePermissionAddress"].ToString();
            WebConsts.WinHandleProtalAddress = ConfigurationManager.AppSettings["WinHandleProtalAddress"].ToString();
            WebConsts.WinHandleLoginAddress = ConfigurationManager.AppSettings["WinHandleLoginAddress"].ToString();
            WebConsts.WinHandleLogoutAddress = ConfigurationManager.AppSettings["WinHandleLogoutAddress"].ToString();
            WebConsts.WinHandleValidatationAddress = ConfigurationManager.AppSettings["WinHandleValidatationAddress"].ToString();
            WebConsts.WinHandleUpdateInfoAddress = ConfigurationManager.AppSettings["WinHandleUpdateInfoAddress"].ToString();
            WebConsts.WinTmpException = ConfigurationManager.AppSettings["WinTmpException"].ToString();
        }
    }
}
