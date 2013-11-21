using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.BusiServices.Interfaces
{
    public interface IFunctionBusiService
    {
        ComRet SaveFunction(Function func);
        ComRet SaveFunctions(List<Function> funcs);
        ComRet GetFunctions(int mid);
        ComRet GetFunction(int id);
    }
}
