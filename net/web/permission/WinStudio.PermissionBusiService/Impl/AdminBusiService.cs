using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Permission.Models;

namespace WinStudio.PermissionBusiService
{
    public class AdminBusiService : BaseBusiService<Administrator>
    {
        public static bool IsDatabaseExist(SysConfig config)
        {
            PermissionDbContext context = new PermissionDbContext(config.UseWebConfig, config.NameOrConnectionString);
            return context.Database.Exists();
        }

        public static bool CreateDatabase()
        {
            PermissionDbContext context = new PermissionDbContext();
            return context.Database.CreateIfNotExists();
        }

        public ComRet Login(string account, string pwd)
        {
            pwd = pwd.ToMD5();
            Administrator admin = Get(a => a.Account == account);
            if (admin == null || admin.Pwd != pwd) return Result(false);
            string token = string.Format("{0}{1}{2}", Guid.NewGuid(), account, DateTime.Now.ToString("yyyyMMddHHmmssffff")).ToMD5();
            //Reception.AddSession(account, token, syscode, sysauth);
            return Result(true, token, 0, admin);
        }
    }
}
