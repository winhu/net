using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;

namespace WinStudio.Permission.BusiServices.Interfaces
{
    public interface IRFunctionBusiService
    {
        ComRet GetRFunctions(int rid);
        ComRet GetFunction(int id);

        ComRet DeleteFunction(int id);
    }
}
