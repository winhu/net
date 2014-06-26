using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStuido.Permission.IBusiness.Guidance
{
    public interface ICookieInfo
    {
        string Domain { get; set; }

        string Name { get; set; }

        string Value { get; set; }

        DateTime LastTime { get; }

        bool IsExpired { get; }
        bool IsExpiredAndUpdate { get; }

        void Reset();
        void Expired();
    }
}
