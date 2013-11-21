using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WinStudio.ComResult;
using WinStudio.Permission.BusiServices.Interfaces;
using WinStudio.Permission.Common.Framework.Abstract;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.BusiServices.Impl
{
    [Export(typeof(IFunctionBusiService))]
    public class FunctionBusiService : AbstractPermissionBusiService<Function>, IFunctionBusiService
    {

        public ComRet SaveFunction(Function func)
        {
            if (!func.IsValid()) return Result(func.ValidMsg);
            //func.SetKey(PermissionUtility.PermissionEncrypter(func.Address));
            Save(func);
            return Result();
        }

        public ComRet SaveFunctions(List<Function> funcs)
        {
            foreach (Function f in funcs)
            {
                this.Get<PermissionDbContext>().Functions.Add(f);
            }
            int count = this.Get<PermissionDbContext>().SaveChanges();
            return Result(count);
        }


        public ComRet GetFunctions(int mid)
        {
            return Result(GetAll(f => f.SysModuleID == mid));
        }


        public ComRet GetFunction(int id)
        {
            return Result(GetById(id));
        }
    }
}
