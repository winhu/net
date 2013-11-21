using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Framework.Web.Services;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.BusiServices.Interfaces
{
    public interface ISysModuleBusiService : IWinBusiService
    {
        ComRet GetSysModule(int id);
        ComRet SaveSysModule(SysModule module);
        ComRet Exists(int id);
        ComRet GetAllUsingSysModules();
        ComRet AutoGeneratePermission(string xmlpath, string syscode, string area);
        ComRet AutoGeneratePermission(string xmlpath, int mid, string area);
        ComRet GetUserPermission(string sysauth, string syscode, string account);
    }
}
