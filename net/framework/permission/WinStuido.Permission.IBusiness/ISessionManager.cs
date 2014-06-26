using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStuido.Permission.IBusiness
{
    public interface ISessionManager
    {
        void Expire(string cookiename, string token);

        bool IsExpired(string cookiename, string token);

        List<IUserInfo> GetAllUser();
        List<IUserInfo> GetUsers(string cookiename);
        int CountAll();
        int CountAll(string cookiename);

        string Add(IUserInfo user);
        string Add(IUserInfo user, string securityToken);
    }
}
