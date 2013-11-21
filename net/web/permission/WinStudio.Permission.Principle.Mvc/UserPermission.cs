using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WinStudio.Permission.Principle.Mvc;

namespace WinStudio.Permission.Principle.Mvc
{
    internal class UserPermission : IUserPermission
    {
        public UserPermission(string account) { this.account = account; }
        private string account = string.Empty;
        public string Account { get { return account; } }


        private List<Permission> Permissions = new List<Permission>();
        public void UpdatePermission(string sysauth, string syscode, string account, string roles, string funckeys)
        {
            if (account != Account) return;
            Permissions.RemoveAll(p => p.SysAuth == sysauth && p.SysCode == syscode);
            Permission permission = new Permission(sysauth, syscode);
            foreach (string role in roles.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                permission.AddRole(role);
            }
            foreach (string func in funckeys.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                permission.AddFunc(func);
            }
            Permissions.Add(permission);
        }

        public void UpdatePermission(string permissions)
        {
            if (string.IsNullOrEmpty(permissions)) return;
            string[] groups = permissions.Split(',');
            if (groups == null || groups.Length != 5) return;
            UpdatePermission(groups[0], groups[1], groups[2], groups[3], groups[4]);
        }


        public bool HasPermission(string sysauth, string syscode, string account, string roles, string key, string busicode)
        {
            if (string.IsNullOrEmpty(sysauth) ||
                string.IsNullOrEmpty(key) ||
                string.IsNullOrEmpty(syscode) ||
                string.IsNullOrEmpty(account) ||
                Account != account) return false;

            Permission permission = Permissions.SingleOrDefault(p => p.SysAuth == sysauth && p.SysCode == syscode);
            if (permission == null) return false;
            if (string.IsNullOrEmpty(busicode))
                return permission.HasRole(roles) || permission.HasFunc(key);
            else
                return permission.HasRole(roles, busicode) && permission.HasFunc(key);
        }

        public bool HasPermission(System.Web.HttpContextBase context, PermissionInfo info, string roles)
        {
            return HasPermission(context.GetAuthority(), info.SysCode, info.UserAccount, roles, context.GetPermissionKey(), info.BusiCode);
        }
    }
}
