using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Principle
{
    public class PermissionConfig
    {
        public PermissionConfig(string accont, string syscode, string sysauth)
        {
            SysAuth = sysauth;
            Account = accont;
            SysCode = syscode;
        }
        public PermissionConfig(string token)
        {
            this.token = token;
        }
        public string SysCode { get; set; }
        public string SysAuth { get; set; }
        public string Account { get; set; }

        private string token;
        public string Token
        {
            get
            {
                if (string.IsNullOrEmpty(Account) || string.IsNullOrEmpty(SysAuth) || string.IsNullOrEmpty(SysCode))
                    throw new Exception("Account, SysAuth and SysCode can not be null");
                return string.Format("PermissionConfig_{0}_{1}_{2}", Account, SysAuth, SysCode).ToSHA1();
            }
        }
        public bool IsValidConfig
        {
            get
            {
                if (string.IsNullOrEmpty(token)) return false;
                return token == Token;
            }
        }
    }
}
