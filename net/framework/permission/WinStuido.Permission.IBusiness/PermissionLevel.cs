using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStuido.Permission.IBusiness
{
    public enum PermissionLevel
    {
        /// <summary>
        /// 无需登录即可识别用户
        /// </summary>
        [Description("安全识别码")]
        SecurityKey = 0,

        /// <summary>
        /// 要求必须登录
        /// </summary>
        [Description("安全秘钥 ")]
        SecurityToken = 1
    }
}
