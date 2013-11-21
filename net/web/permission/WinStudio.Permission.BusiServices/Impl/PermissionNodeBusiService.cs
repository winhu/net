using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Framework.Web.Services;
using WinStudio.Permission.BusiServices.Interfaces;
using WinStudio.Permission.Common.Framework.Abstract;
using WinStudio.Permission.Models;
using WinStudio.Permission.Principle;

namespace WinStudio.Permission.BusiServices.Impl
{
    [Export(typeof(IPermissionBusiService))]
    public class PermissionBusiService : IPermissionBusiService
    {

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="sysauth">系统域标识</param>
        /// <param name="syscode">系统代码</param>
        /// <param name="account">用户账号</param>
        /// <returns>返回当前系统下权限字符串：account,syscode,role1|role2,funckey1|funckey2|funckey3</returns>
        public ComRet GetUserPermission(PermissionConfig config)
        {
            IRoleBusiService roleServ = new RoleBusiService();// GetService<IRoleBusiService>();//   new RoleBusiService();
            var ret = roleServ.GetPermissions(config.SysAuth, config.SysCode, config.Account);// .GetAll(r => r.Module.Code == syscode && r.Module.Authority == sysauth && r.Customers.Any(c => c.Account == account)).ToList();
            if (!ret.Ret)
                return ret;
            var rs = ret.Instance<List<Role>>();
            if (rs == null || rs.Count == 0) return new ComRet(false);
            List<string> roles = new List<string>();
            List<string> funcs = new List<string>();
            rs.ForEach(delegate(Role role)
            {
                roles.Add(role.CBC);
                role.CollectFunctionKeys(funcs);
            });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0},{1},{2},{3},{4}", config.SysAuth, config.SysCode, config.Account, roles.Join('|'), funcs.Join('|'));
            return new ComRet(true, sb.ToString());
        }
    }
}
