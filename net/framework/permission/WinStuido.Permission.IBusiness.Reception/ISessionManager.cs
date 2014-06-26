using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStuido.Permission.IBusiness.Core;

namespace WinStuido.Permission.IBusiness.Reception
{
    public interface ISessionManager
    {
        void Add(IUserInfo user, DateTime lasttime);
        void Add(IUserInfo user, DateTime lasttime, DateTime logintime, string securityToken);

        void Expire(string token);

        bool IsExpired(string token, string dynamictoken);

        bool IsLegal(string token);

        IUserInfo[] GetAllUser();
        IUserInfo[] GetValidUsers();
        IUserInfo[] GetLegalUsers();

        int CountAll();
        int CountLegal();
        int CountValid();

    }
}
