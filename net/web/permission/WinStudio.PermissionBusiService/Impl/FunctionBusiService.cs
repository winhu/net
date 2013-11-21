using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WinStudio.ComResult;
using WinStudio.Permission.Models;

namespace WinStudio.PermissionBusiService
{
    public class FunctionBusiService : BaseBusiService<Function>
    {

        public ComRet SaveFunction(Function func)
        {
            if (!func.IsValid()) return Result(func.ValidMsg);
            //func.SetKey(PermissionUtility.PermissionEncrypter(func.Address));
            Save(func);
            return Result();
        }

        public int SavFunctions(List<Function> funcs)
        {
            foreach (Function f in funcs)
            {
                this.Get<PermissionDbContext>().Functions.Add(f);
            }
            int count = this.Get<PermissionDbContext>().SaveChanges();
            return count;
        }

    }
}
