using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission
{
    public interface ISessionManager
    {
        void Expire(string token);

        ISession Get(string token);
        bool IsExpired(string token);

        List<UserInfo> GetList();
        int Count { get; }

        string Add(string id, string account, string name, string module, string host);
        string Add(string id, string account, string name);
        string Add(string id, string account);
    }
}
