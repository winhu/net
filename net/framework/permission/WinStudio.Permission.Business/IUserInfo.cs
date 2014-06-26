using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Business
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
        /// <summary>
        /// 上次更新时间
        /// </summary>
        //DateTime LastTime { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        //DateTime LoginTime { get; set; }
    }
}
