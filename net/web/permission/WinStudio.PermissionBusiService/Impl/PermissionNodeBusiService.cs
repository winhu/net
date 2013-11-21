using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Permission.Models;

namespace WinStudio.PermissionBusiService
{
    public class PermissionBusiService
    {

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="sysauth">系统域标识</param>
        /// <param name="syscode">系统代码</param>
        /// <param name="account">用户账号</param>
        /// <returns>返回当前系统下权限字符串：account,syscode,role1|role2,funckey1|funckey2|funckey3</returns>
        public string GetUserPermission(string sysauth, string syscode, string account)
        {
            RoleBusiService roleServ = new RoleBusiService();
            var rs = roleServ.GetAll(r => r.System.Code == syscode && r.System.Authority == sysauth && r.Customers.Any(c => c.Account == account)).ToList();

            if (rs == null || rs.Count == 0) return string.Empty;
            List<string> roles = new List<string>();
            List<string> funcs = new List<string>();
            rs.ForEach(delegate(Role role)
            {
                roles.Add(role.CBC);
                role.CollectFunctionKeys(funcs);
            });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0},{1},{2},{3},{4}", sysauth, syscode, account, roles.Join('|'), funcs.Join('|'));
            return sb.ToString();
        }
    }
}
