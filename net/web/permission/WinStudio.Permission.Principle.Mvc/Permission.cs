using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Principle.Mvc
{
    internal class Permission
    {
        public Permission(string sysauth, string syscode)
        {
            this.sysauth = sysauth;
            this.syscode = syscode;
            Roles = new List<string>();
            Functions = new List<string>();
        }
        private string sysauth = string.Empty;
        private string syscode = string.Empty;
        public string SysAuth { get { return sysauth; } }
        public string SysCode { get { return syscode; } }
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
