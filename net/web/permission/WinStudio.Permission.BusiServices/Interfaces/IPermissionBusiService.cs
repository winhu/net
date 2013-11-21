using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Permission.Principle;

namespace WinStudio.Permission.BusiServices.Interfaces
{
    public interface IPermissionBusiService
    {
        ComRet GetUserPermission(PermissionConfig config);
    }
}
