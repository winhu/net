using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WinStudio.Permission.Principle;
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
        internal static CookieContainer ToCookieContainer(this HttpCookieCollection cookies)
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
        internal static void SaveUserPermission(this HttpContextBase context, string account)
        {
            IUserPermission user = new UserPermission(account);
            context.Session[UserPermissionKey] = user;
        }

        internal static IUserPermission GetUserPermission(this HttpContextBase context, string account = null)
        {
            if (context.Session[UserPermissionKey] != null)
            {
                IUserPermission user = context.Session[UserPermissionKey] as IUserPermission;
                if (string.IsNullOrEmpty(account) || user.Account == account)
                {
                    return user;
                }
            }
            if (string.IsNullOrEmpty(account)) return null;
            var u = new UserPermission(account);
            context.Session[UserPermissionKey] = u;
            return u;

        }

        internal static string GetAuthority(this HttpContextBase context)
        {
            return string.Format("{0}://{1}", context.Request.Url.Scheme, context.Request.Url.Authority);
        }
        internal static string GetPermissionKey(this HttpContextBase context)
        {
            return context.Request.Url.GetAbsUrl().ByPermissionEncrypter();
        }


        public static PermissionConfig BuildPermissionConfig(this HttpContextBase context, PermissionInfo info)
        {
            string auth = context.GetAuthority();
            PermissionConfig config = new PermissionConfig(info.UserAccount, auth, info.SysCode);

            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("Account", config.Account);
            nvc.Add("SysCode", config.SysCode);
            nvc.Add("SysAuth", config.SysAuth);
            nvc.Add("Token", config.Token);
            return config;
        }

        public static PermissionConfig GetPermissionConfig(this HttpContextBase context)
        {
            string Account = context.Request.Headers["Account"];
            string SysCode = context.Request.Headers["SysCode"];
            string SysAuth = context.Request.Headers["SysAuth"];
            string Token = context.Request.Headers["Token"];
            PermissionConfig config = new PermissionConfig(Token);// { SysCode = SysCode, SysAuth = SysAuth, Account = Account };
            config.SysCode = SysCode;
            config.SysAuth = SysAuth;
            config.Account = Account;
            return config;
        }
    }
}
