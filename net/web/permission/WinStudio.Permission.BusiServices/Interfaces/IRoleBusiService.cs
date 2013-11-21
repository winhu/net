using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.BusiServices.Interfaces
{
    public interface IRoleBusiService
    {
        ComRet SaveRole(Role role);
        ComRet GetRoles(int mid);
        ComRet GetRole(int mid, int pid, int id);
        ComRet GetRole(int id);
        ComRet CopyFunctions(int id, string funcs, List<Function> all);
        ComRet GetPermissions(string moduleauth, string modulecode, string account);
    }
}
