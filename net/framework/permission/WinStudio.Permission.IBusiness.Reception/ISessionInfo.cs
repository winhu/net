using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Permission.IBusiness.Core;

namespace WinStudio.Permission.IBusiness.Reception
{
    public interface ISessionInfo : IUserInfo
    {
        /// <summary>
        /// 动态安全秘钥
        /// </summary>
        string DynamicToken { get; set; }
        /// <summary>
        /// 上次更新时间
        /// </summary>
        DateTime LastTime { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        DateTime LoginTime { get; set; }

        //bool IsExpired { get; }
        //bool IsExpiredAndUpdate { get; }

        //void Reset();
        //void Expired();
        //ISessionInfo Clone();
    }
}
