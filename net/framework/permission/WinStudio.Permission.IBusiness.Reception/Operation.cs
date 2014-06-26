using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace System.Web
{
    public static class Operation
    {
        /// <summary>
        /// convert HttpContext to HttpContextBase
        /// </summary>
        /// <param name="context">context</param>
        /// <returns></returns>
        public static HttpContextBase GotBase(this HttpContext context)
        { return new HttpContextWrapper(context); }

        #region For Cookie

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
        /// <summary>
        /// get cookie from http context
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        /// <returns></returns>
        public static HttpCookie GetPermissionCookie(this HttpContext context, string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            return context.GotBase().GetPermissionCookie(name);
        }
        /// <summary>
        /// get cookie from http context
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        /// <returns></returns>
        public static HttpCookie GetPermissionCookie(this HttpContextBase context, string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            return context.Request.Cookies[name];
        }
        /// <summary>
        /// get cookie value from http context
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        /// <returns></returns>
        public static string GetPermissionCookieValue(this HttpContextBase context, string name)
        {
            if (string.IsNullOrEmpty(name)) return string.Empty;
            HttpCookie cookie = context.GetPermissionCookie(name);
            if (cookie == null) return string.Empty;
            return cookie.Value;
        }
        /// <summary>
        /// get cookie value from http context
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        /// <returns></returns>
        public static string GetPermissionCookieValue(this HttpContext context, string name)
        {
            return context.GotBase().GetPermissionCookieValue(name);
        }
        /// <summary>
        /// has cookie in http context
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        /// <returns></returns>
        public static bool HasPermissionCookie(this HttpContextBase context, string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            return context.Request.Cookies.AllKeys.Contains(name);
        }
        /// <summary>
        /// has cookie in http context
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        /// <returns></returns>
        public static bool HasPermissionCookie(this HttpContext context, string name)
        {
            return context.GotBase().HasPermissionCookie(name);
        }
        /// <summary>
        /// has cookie value in http context
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="val">cookie value</param>
        /// <returns></returns>
        public static bool HasPermissionCookieValue(this HttpContextBase context, string val)
        {
            if (string.IsNullOrEmpty(val)) return false;
            foreach (string key in context.Request.Cookies.AllKeys)
            {
                if (context.Request.Cookies[key].Value.Equals(val))
                    return true;
            } return false;
        }
        /// <summary>
        /// has cookie value in http context
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="val">cookie value</param>
        /// <returns></returns>
        public static bool HasPermissionCookieValue(this HttpContext context, string val)
        {
            return context.GotBase().HasPermissionCookie(val);
        }
        /// <summary>
        /// save cookie in http context
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        /// <param name="val">cookie value</param>
        /// <param name="domain">cookie domain</param>
        /// <param name="timeout">cookie timeout(minutes, if 0, means dont set Expires)</param>
        /// <param name="httponly">is http only</param>
        /// <param name="secure">use ssl(only used for https)</param>
        public static void SavePermissionCookie(this HttpContextBase context, string name, string val, string domain, int timeout = 0, bool httponly = false, bool secure = false)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(val)) return;
            HttpCookie cookie = context.GetPermissionCookie(name);
            if (cookie == null) cookie = new HttpCookie(name);
            cookie.Value = val;
            if (!string.IsNullOrEmpty(domain))
                cookie.Domain = domain;
            if (timeout != 0)
                cookie.Expires = DateTime.Now.AddMinutes(timeout);
            cookie.HttpOnly = httponly;
            cookie.Secure = secure;
            context.Response.SetCookie(cookie);
        }
        /// <summary>
        /// save cookie in http context
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        /// <param name="val">cookie value</param>
        /// <param name="domain">cookie domain</param>
        /// <param name="timeout">cookie timeout(minutes, if 0, means dont set Expires)</param>
        /// <param name="httponly">is http only</param>
        /// <param name="secure">use ssl(only used for https)</param>
        public static void SavePermissionCookie(this HttpContext context, string name, string val, string domain, int timeout = 0, bool httponly = false, bool secure = false)
        {
            context.GotBase().SavePermissionCookie(name, val, domain, timeout, httponly, secure);
        }
        /// <summary>
        /// remove cookie from http context by name
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        public static void RemovePermissionCookie(this HttpContextBase context, string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (context.HasPermissionCookie(name))
            {
                HttpCookie cookie = context.GetPermissionCookie(name);
                cookie.Expires = DateTime.Now.AddYears(-1);
                context.Response.SetCookie(cookie);
            }
        }
        /// <summary>
        /// remove cookie from http context by name
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        public static void RemovePermissionCookie(this HttpContext context, string name)
        {
            context.GotBase().RemovePermissionCookie(name);
        }
        /// <summary>
        /// update cookie Expires
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        /// <param name="domain">domain</param>
        /// <param name="timeout">expired time(minutes)</param>
        public static void UpdatePermissionCookieExpires(this HttpContextBase context, string name, string domain, int timeout)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (context.HasPermissionCookie(name))
            {
                HttpCookie cookie = context.GetPermissionCookie(name);
                if (!string.IsNullOrEmpty(domain))
                    cookie.Domain = domain;
                cookie.Expires = DateTime.Now.AddMinutes(timeout);
                context.Response.SetCookie(cookie);
            }
        }
        /// <summary>
        /// update cookie Expires
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        /// <param name="domain">domain</param>
        /// <param name="timeout">expired time(minutes)</param>
        public static void UpdatePermissionCookieExpires(this HttpContext context, string name, string domain, int timeout)
        {
            context.GotBase().UpdatePermissionCookieExpires(name, domain, timeout);
        }
        /// <summary>
        /// convert httpcookie to namevalue string like name=n&value=v&path=p&domain=d
        /// </summary>
        /// <param name="cookie">http cookie</param>
        /// <returns></returns>
        public static string ToPermissionNVString(this HttpCookie cookie)
        {
            return string.Format("name={0}&value={1}&path={2}&domain={3}", cookie.Name, cookie.Value, cookie.Path, cookie.Domain);
        }
        #endregion

        /// <summary>
        /// get param from form or query string
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">param name</param>
        /// <returns></returns>
        public static string GetPermissionParam(this HttpContextBase context, string name)
        {
            if (context.Request.Form.AllKeys.Contains(name)) return context.Request.Form[name];
            if (context.Request.QueryString.AllKeys.Contains(name)) return context.Request.QueryString[name];
            return string.Empty;
        }

        /// <summary>
        /// get token from cookie(UrlDecoded), form or query string
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">token name</param>
        /// <returns></returns>
        public static string GetPermissionToken(this HttpContextBase context, string name)
        {
            var token = context.GetPermissionCookieValue(name);
            if (string.IsNullOrEmpty(token))
                return context.GetPermissionParam(name);
            return context.Server.UrlDecode(token);
        }


        public static string GetAuthority(this HttpContextBase context)
        {
            return string.Format("{0}://{1}", context.Request.Url.Scheme, context.Request.Url.Authority);
        }
        public static string GetAuthority(this Uri uri)
        {
            return string.Format("{0}://{1}", uri.Scheme, uri.Authority);
        }
        public static string GetAbsUrl(this Uri uri)
        {
            return string.Format("{0}://{1}{2}", uri.Scheme, uri.Authority, uri.AbsolutePath);
        }
    }

    public class HttpHelper
    {
        /// <summary>
        /// send HttpWebRequest
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="data">data(k1=v1&k2=v2)</param>
        /// <param name="cookies">HttpCookieCollection</param>
        /// <param name="method">POST or GET</param>
        /// <param name="encoding">encoding(utf-8, gbk)</param>
        /// <returns></returns>
        public static string SendPermissionRequest(string url, string data, HttpCookieCollection cookies, string method = "POST", string encoding = "utf-8")
        {
            Encoding _encoding = Encoding.GetEncoding(encoding);
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
            Request.CookieContainer = cookies.ToCookieContainer();
            Request.Method = method;
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.AllowAutoRedirect = true;
            if (!string.IsNullOrEmpty(data))
            {
                byte[] postdata = _encoding.GetBytes(data);
                using (Stream newStream = Request.GetRequestStream())
                {
                    newStream.Write(postdata, 0, postdata.Length);
                }
            }
            using (HttpWebResponse response = (HttpWebResponse)Request.GetResponse())
            {

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, _encoding, true))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

    }

}
