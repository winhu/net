using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Common.Framework
{
    public class Consts
    {
        public const string WinPermissionSystemConfigKey = "Win_Permission_System_Config_Key";
        public const string TokenName = "WinPermission";
        public const string TokenDomain = "";
        public const int TokenTimeout = 20;
        public const string WinSysCode = "WinPermission";
        public const string HandlePermissionUpdateAddress = "http://localhost:9060/Permission/GetUserPermission";
        public const string HandleUnauthorizedAddress = "http://localhost:9000";
        public const string HandleLocalUnauthorizedAddress = "http://localhost:9050/Account/Login";

        public const string TmpException = "TempData_Exception";
    }
}
