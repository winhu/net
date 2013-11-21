using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Permission.Principle.Mvc;

namespace WinStudio.Permission.Principle.Implementions
{
    public class UserPermission : IUserPermission
    {
        public UserPermission(string account) { this.account = account; }
        private string account = string.Empty;
        public string Account { get { return account; } }

        public string Token { get; set; }

        private List<Permission> Permissions = new List<Permission>();
        public void UpdatePermission(string sysauth, string roles, string funckeys)
        {
            Permissions.RemoveAll(p => p.SysAuth == sysauth);
            Permission permission = new Permission(sysauth);
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

        public bool HasPermission(string sysauth, string roles, string key, string busicode)
        {
            if (string.IsNullOrEmpty(sysauth) || string.IsNullOrEmpty(key)) return false;
            Permission permission = Permissions.SingleOrDefault(p => p.SysAuth == sysauth);
            if (permission == null) return false;
            if (string.IsNullOrEmpty(busicode))
                return permission.HasRole(roles) || permission.HasFunc(key);
            else
                return permission.HasRole(roles, busicode) && permission.HasFunc(key);
        }


        public void UpdatePermission(string permissions)
        {
            if (string.IsNullOrEmpty(permissions)) return;
            string[] groups = permissions.Split(',');
            if (groups == null || groups.Length != 4) return;
            if (Account != groups[0]) return;
            UpdatePermission(groups[1], groups[2], groups[3]);
        }
    }
}
