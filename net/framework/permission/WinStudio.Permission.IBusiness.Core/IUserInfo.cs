using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStuido.Permission.IBusiness.Core
{
    public interface IUserInfo
    {
        /// <summary>
        /// 用户安全识别码
        /// </summary>
        string SecurityKey { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        string Account { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 用户Email
        /// </summary>
        string Email { get; set; }
        /// <summary>
        /// 用户的当前IP
        /// </summary>
        string IP { get; set; }
        /// <summary>
        /// 用户注册时间
        /// </summary>
        DateTime RegisterdTime { get; set; }
        /// <summary>
        /// 用户注册来源
        /// </summary>
        string Origin { get; set; }
    }
}
