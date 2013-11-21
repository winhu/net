using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission
{
    public interface ISession
    {
        string Token { get; set; }
        string Id { get; set; }
        string Account { get; set; }
        string Name { get; set; }
        string Module { get; set; }
        DateTime LastTime { get; set; }
        string Host { get; set; }

        bool IsExpired { get; }
        bool IsExpiredAndUpdate { get; }

        void Reset();
        void Expired();
        ISession Clone();
    }
}
