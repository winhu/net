using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStuido.Permission.IBusiness.Reception
{
    public class SecurityManager
    {
        public string GenDynamicToken(ISessionInfo user)
        {
            return string.Format("{0}{1}", user.Account, (user.LastTime - user.LoginTime).Ticks, (user.LoginTime - user.RegisterdTime).Ticks).ToMD5();
        }

        public bool ValidDynamicToken(ISessionInfo user, string targetoken)
        {
            if (string.IsNullOrEmpty(targetoken) || user == null) return false;
            return targetoken.Equals(GenDynamicToken(user));
        }
        //public void UpdateDynamicToken(ISessionInfo user)
        //{
        //    if(user.LastTime
        //}
    }
}
