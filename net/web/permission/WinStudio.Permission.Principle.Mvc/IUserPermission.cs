using System;
using System.Web;
using System.Web.Mvc;

namespace WinStudio.Permission.Principle.Mvc
{
    internal interface IUserPermission
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        string Account { get; }
        
        /// <summary>
        /// 更新用户权限
        /// </summary>
        /// <param name="sysauth">系统域标识</param>
        /// <param name="roles">所有角色</param>
        /// <param name="funckeys">所有功能</param>
        void UpdatePermission(string sysauth, string syscode, string account, string roles, string funckeys);

        /// <summary>
        /// 更新用户权限
        /// </summary>
        /// <param name="permissions">权限字符串</param>
        void UpdatePermission(string permissions);

        /// <summary>
        /// 判断是否具有权限
        /// </summary>
        /// <param name="sysauth">系统域标识</param>
        /// <param name="syscode">系统代码</param>
        /// <param name="account">用户账号</param>
        /// <param name="roles">所有指定角色</param>
        /// <param name="key">当前功能</param>
        /// <param name="busicode">当前业务标识</param>
        /// <returns></returns>
        bool HasPermission(string sysauth, string syscode, string account, string roles, string key, string busicode);

        /// <summary>
        /// 判断是否具有权限
        /// </summary>
        /// <param name="context">当前请求上下文</param>
        /// <param name="info">权限信息</param>
        /// <param name="roles">所有指定角色</param>
        /// <returns></returns>
        bool HasPermission(HttpContextBase context, PermissionInfo info, string roles);
    }
}
