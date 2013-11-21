using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Principle.Mvc
{
    public interface IUserPermission
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        string Account { get; }

        string Token { get; set; }

        /// <summary>
        /// 更新用户权限
        /// </summary>
        /// <param name="sysauth">系统域标识</param>
        /// <param name="roles">所有角色</param>
        /// <param name="funckeys">所有功能</param>
        void UpdatePermission(string sysauth, string roles, string funckeys);

        /// <summary>
        /// 更新用户权限
        /// </summary>
        /// <param name="permissions">权限字符串</param>
        void UpdatePermission(string permissions);

        /// <summary>
        /// 判断用户是否具有权限
        /// </summary>
        /// <param name="sysauth">系统域标识</param>
        /// <param name="roles">所有指定角色</param>
        /// <param name="key">当前功能</param>
        /// <param name="busicode">当前业务标识</param>
        /// <returns></returns>
        bool HasPermission(string sysauth, string roles, string key, string busicode);
    }
}
