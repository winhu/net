using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Business
{

    public interface ISecurityManager
    {
        string GenSecurityKey(IUserInfo user);
        bool ValidSecurityKey(IUserInfo user, string securitykey);

        string GenDynamicToken(IUserInfo user, string code);
        bool ValidDynamicToken(IUserInfo user, string code, string dynamictoken);
    }

}
