using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Permission.Business;

namespace WinStudio.Permission
{
    public class SecurityManager : ISecurityManager
    {
        public string GenSecurityKey(IUserInfo user)
        {
            if (user == null) return string.Empty;
            return string.Format("{0}{1}", user.Id, user.Account, user.Email, user.RegisterdTime.ToString("yyyyMMddHHmmss"));
        }

        public bool ValidSecurityKey(IUserInfo user, string securitykey)
        {
            if (string.IsNullOrEmpty(securitykey)) return false;
            return securitykey.Equals(GenSecurityKey(user));
        }

        public string GenDynamicToken(IUserInfo user, string code)
        {
            return string.Format("{0}{1}", GenSecurityKey(user), code.ToMD5());
        }

        public bool ValidDynamicToken(IUserInfo user, string code, string dynamictoken)
        {
            if (string.IsNullOrEmpty(dynamictoken)) return false;
            return dynamictoken.Equals(GenDynamicToken(user, code));
        }
    }
}
