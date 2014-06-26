using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStuido.Permission.IBusiness
{
    public interface ISessionInfo : IUserInfo
    {
        DateTime LastTime { get; }

        bool IsExpired { get; }
        bool IsExpiredAndUpdate { get; }

        void Reset();
        void Expired();
        ISessionInfo Clone();

        List<ICookieInfo> Cookies { get; set; }
    }
}
