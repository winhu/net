using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.BusiServices.Interfaces
{
    public interface IAdministratorBusiService
    {
        //ComRet IsDatabaseExist(SysConfig config);
        //ComRet CreateDatabase();
        ComRet Login(string account, string pwd);
    }
}
