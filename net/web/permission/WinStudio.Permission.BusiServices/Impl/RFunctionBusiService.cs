using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Permission.BusiServices.Interfaces;
using WinStudio.Permission.Common.Framework.Abstract;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.BusiServices.Impl
{
    [Export(typeof(IRFunctionBusiService))]
    public class RFunctionBusiService : AbstractPermissionBusiService<RFunc>, IRFunctionBusiService
    {
        public ComRet GetRFunctions(int rid)
        {
            return Result(GetAll(f => f.RoleID == rid).ToList());
        }

        public ComRet GetFunction(int id)
        {
            return Result(GetById(id));
        }


        public ComRet DeleteFunction(int id)
        {
            return Result(Delete(id));
        }
    }
}
