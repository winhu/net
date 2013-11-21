using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WinStudio.Permission.Principle.Mvc;

namespace System.Web.Mvc
{
    public static class HttpExtension
    {
        internal static string GeToken(this HttpContextBase context, string name)
        {
            if (!context.Request.Cookies.AllKeys.Contains(name))
            {
                if (context.Request.Form.AllKeys.Contains(name))
                    return context.Request.Form[name];
                return context.Request.QueryString[name];
            }
            return context.Server.HtmlDecode(context.Request.Cookies[name].Value);
        }

        internal static void SaveCookie(this HttpContextBase context, string name, string val, string domain)
        {
            HttpCookie cookie = new HttpCookie(name, val);
            if (!string.IsNullOrEmpty(domain))
                cookie.Domain = domain;
            context.Response.SetCookie(cookie);
        }

        /// <summary>
        /// convert HttpCookieCollection to CookieContainer
        /// </summary>
        /// <param name="cookies">HttpCookieCollection</param>
        /// <returns></returns>
        public static CookieContainer ToCookieContainer(this HttpCookieCollection cookies)
        {
            CookieContainer cc = new CookieContainer();
            if (cookies == null || cookies.Count == 0) return cc;
            foreach (string key in cookies.AllKeys)
            {
                HttpCookie cookie = cookies.Get(key);
                Cookie ck = new Cookie(cookie.Name, cookie.Value);
                ck.Path = cookie.Path;
                ck.Domain = cookie.Domain ?? "localhost";
                ck.Expires = cookie.Expires;
                ck.HttpOnly = cookie.HttpOnly;
                ck.Secure = cookie.Secure;
                cc.Add(ck);
            }
            return cc;
        }

        private const string UserPermissionKey = "Win_User_Permission_Key";
        public static void SaveUserPermission(this HttpContextBase context, string account)
        {
            IUserPermission user = new UserPermission(account);
            context.Session[UserPermissionKey] = user;
        }

        internal static IUserPermission GetUserPermission(this HttpContextBase context)
        {
            if (context.Session[UserPermissionKey] == null) return null;
            return context.Session[UserPermissionKey] as IUserPermission;
        }
        internal static void RemoveSessionUser(this HttpContextBase context)
        {
            context.Session.Remove(UserPermissionKey);
        }

        internal static string GetUserAccount(this HttpContextBase context)
        {
            IUserPermission user = context.GetUserPermission();
            if (user == null) return null;
            return user.Account;
        }
        internal const string WinPermissionBusiCode = "WinPermissionBusiCode";
        internal static string GetSystemAuthority(this HttpContextBase context)
        {
            return context.Request.Url.Authority;
        }
        internal static void SetWinPermissionBusiCode(this HttpContextBase context, string busicode)
        {
            context.Session[WinPermissionBusiCode] = busicode;
        }
        internal static string GetWinPermissionBusiCode(this HttpContextBase context)
        {
            return context.Session[WinPermissionBusiCode].ToString();
        }
        internal static bool NeedWinPermissionBusiCode(this HttpContextBase context)
        {
            return null != context.Session[WinPermissionBusiCode];
        }
        internal static string GetWinPermissionBusiCodeValue(this HttpContextBase context)
        {
            string code = context.Session[WinPermissionBusiCode].ToString();
            return context.Request.QueryString[code];
        }

        public static void UpdatePermission(this HttpContextBase context, string permission)
        {
            IUserPermission user = context.GetUserPermission();
            if (user == null || string.IsNullOrEmpty(permission)) return;
            string[] groups = permission.Split(',');
            if (groups == null || groups.Length != 4) return;
            if (user.Account != groups[0]) return;
            user.UpdatePermission(groups[1], groups[2], groups[3]);

        }
        //public static bool HasPermission(this HttpContextBase context, string roles = null)
        //{
        //    IUserPermission user = context.GetSessionUser();
        //    if (user == null) return false;
        //    string auth = Utility.PermissionEncrypter(context.Request.Url.Authority);//.ToMD5();
        //    string key = Utility.PermissionEncrypter(context.Request.Url.AbsolutePath);//.ToMD5();

        //    if (!context.NeedWinPermissionBusiCode())
        //        return user.HasPermission(auth, roles, key);
        //    else return user.HasRole(auth, roles, context.GetWinPermissionBusiCodeValue()) && user.HasFunction(auth, key);
        //}

    }
}
