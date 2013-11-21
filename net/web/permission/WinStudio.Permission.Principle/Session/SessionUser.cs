using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace System.Web.Mvc
{
    public class SessionUser
    {
        public SessionUser(string account) { Account = account; }
        public string Account { get; set; }
        public string Token { get; set; }

        private List<UserPermissiona> Permissions = new List<UserPermissiona>();

        public void AddPermission(string sysauth, string roles, string funckeys)
        {
            Permissions.RemoveAll(p => p.SysAuth == sysauth);
            UserPermissiona permission = new UserPermissiona(sysauth);
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

        public bool HasPermission(string sysauth, string roles, string key)
        {
            if (string.IsNullOrEmpty(sysauth) || string.IsNullOrEmpty(key)) return false;
            UserPermissiona permission = Permissions.SingleOrDefault(p => p.SysAuth == sysauth);
            if (permission == null) return false;
            return permission.HasRole(roles) || permission.HasFunc(key);
        }

        public bool HasRole(string sysauth, string roles, string busicode)
        {
            if (string.IsNullOrEmpty(sysauth) || string.IsNullOrEmpty(roles)) return false;
            UserPermissiona permission = Permissions.SingleOrDefault(p => p.SysAuth == sysauth);
            if (permission == null) return false;
            return permission.HasRole(roles);
        }
        public bool HasFunction(string sysauth, string key)
        {
            if (string.IsNullOrEmpty(sysauth) || string.IsNullOrEmpty(key)) return false;
            UserPermissiona permission = Permissions.SingleOrDefault(p => p.SysAuth == sysauth);
            if (permission == null) return false;
            return permission.HasFunc(key);
        }
    }

    public class UserPermissiona
    {
        public UserPermissiona(string sysauth)
        {
            this.sysauth = sysauth;
            Roles = new List<string>();
            Functions = new List<string>();
        }
        private string sysauth = string.Empty;
        public string SysAuth { get { return sysauth; } }
        private List<string> Roles { get; set; }
        private List<string> Functions { get; set; }

        public void AddRole(string role)
        {
            if (string.IsNullOrEmpty(role)) return;
            Roles.Add(role);
        }
        public void AddFunc(string func)
        {
            if (string.IsNullOrEmpty(func)) return;
            Functions.Add(func);
        }
        public bool HasFunc(string func)
        {
            if (string.IsNullOrEmpty(func)) return false;
            return Functions.Exists(r => r == func);
        }
        public bool HasRole(string roles, string busicode = null)
        {
            if (string.IsNullOrEmpty(roles)) return false;
            var arr = roles.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (!string.IsNullOrEmpty(busicode))
                arr = arr.Select(a => string.Format("{0}.{1}", a, busicode)).ToArray();
            return Roles.Exists(r => arr.Contains(r));
        }
    }
}
