using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace System.Web
{
    public static class Operation
    {

        /// <summary>
        /// 获取当前用户的IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP(this HttpContextBase context)
        {
            // 穿过代理服务器取远程用户真实IP地址
            string Ip = string.Empty;
            if (context.Request.ServerVariables["HTTP_VIA"] != null)
            {
                if (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
                {
                    if (context.Request.ServerVariables["HTTP_CLIENT_IP"] != null)
                        Ip = context.Request.ServerVariables["HTTP_CLIENT_IP"].ToString();
                    else
                        if (context.Request.ServerVariables["REMOTE_ADDR"] != null)
                            Ip = context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                }
                else
                    Ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (context.Request.ServerVariables["REMOTE_ADDR"] != null)
            {
                Ip = context.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }

            if (ip_reg.IsMatch(Ip))
                return Ip;
            else return "127.0.0.1";
        }
        private static Regex ip_reg = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])(\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])){3}$");

        /// <summary>
        /// convert HttpContext to HttpContextBase
        /// </summary>
        /// <param name="context">context</param>
        /// <returns></returns>
        public static HttpContextBase ToBase(this HttpContext context)
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
        public static HttpCookie GetCookie(this HttpContext context, string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            return context.ToBase().GetCookie(name);
        }
        /// <summary>
        /// get cookie from http context
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        /// <returns></returns>
        public static HttpCookie GetCookie(this HttpContextBase context, string name)
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
        public static string GetCookieValue(this HttpContextBase context, string name)
        {
            if (string.IsNullOrEmpty(name)) return string.Empty;
            HttpCookie cookie = context.GetCookie(name);
            if (cookie == null) return string.Empty;
            return cookie.Value;
        }
        /// <summary>
        /// get cookie value from http context
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        /// <returns></returns>
        public static string GetCookieValue(this HttpContext context, string name)
        {
            return context.ToBase().GetCookieValue(name);
        }
        /// <summary>
        /// has cookie in http context
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        /// <returns></returns>
        public static bool HasCookie(this HttpContextBase context, string name)
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
        public static bool HasCookie(this HttpContext context, string name)
        {
            return context.ToBase().HasCookie(name);
        }
        /// <summary>
        /// has cookie value in http context
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="val">cookie value</param>
        /// <returns></returns>
        public static bool HasCookieValue(this HttpContextBase context, string val)
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
        public static bool HasCookieValue(this HttpContext context, string val)
        {
            return context.ToBase().HasCookie(val);
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
        public static void SaveCookie(this HttpContextBase context, string name, string val, string domain, int timeout = 0, bool httponly = false, bool secure = false)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(val)) return;
            HttpCookie cookie = context.GetCookie(name);
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
        public static void SaveCookie(this HttpContext context, string name, string val, string domain, int timeout = 0, bool httponly = false, bool secure = false)
        {
            context.ToBase().SaveCookie(name, val, domain, timeout, httponly, secure);
        }
        /// <summary>
        /// remove cookie from http context by name
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        public static void RemoveCookie(this HttpContextBase context, string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (context.HasCookie(name))
            {
                HttpCookie cookie = context.GetCookie(name);
                cookie.Expires = DateTime.Now.AddYears(-1);
                context.Response.SetCookie(cookie);
            }
        }
        /// <summary>
        /// remove cookie from http context by name
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        public static void RemoveCookie(this HttpContext context, string name)
        {
            context.ToBase().RemoveCookie(name);
        }
        /// <summary>
        /// update cookie Expires
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="name">cookie name</param>
        /// <param name="domain">domain</param>
        /// <param name="timeout">expired time(minutes)</param>
        public static void UpdateCookieExpires(this HttpContextBase context, string name, string domain, int timeout)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (context.HasCookie(name))
            {
                HttpCookie cookie = context.GetCookie(name);
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
        public static void UpdateCookieExpires(this HttpContext context, string name, string domain, int timeout)
        {
            context.ToBase().UpdateCookieExpires(name, domain, timeout);
        }
        /// <summary>
        /// convert httpcookie to namevalue string like name=n&value=v&path=p&domain=d
        /// </summary>
        /// <param name="cookie">http cookie</param>
        /// <returns></returns>
        public static string ToNVString(this HttpCookie cookie)
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
        public static string GetParam(this HttpContextBase context, string name)
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
        public static string GetToken(this HttpContextBase context, string name)
        {
            var token = context.GetCookieValue(name);
            if (string.IsNullOrEmpty(token))
                return context.GetParam(name);
            return context.Server.UrlDecode(token);
        }

        public static string GetHeader(this HttpContextBase context, string name)
        {
            return context.Request.Headers[name];
        }

        /// <summary>
        /// write image on page
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="bytes">image bytes</param>
        public static void WriteImage(this HttpContextBase context, byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return;
            context.Response.Buffer = false;
            context.Response.ContentType = "image/jpg";
            context.Response.AddHeader("Content-Disposition", "attachment;filename=context_response_image");
            context.Response.BinaryWrite(bytes);
            context.Response.End();
        }

        /// <summary>
        /// get file abs path
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="filepath">file relative path</param>
        /// <returns></returns>
        public static string GetServerFullPath(this HttpContextBase context, string filepath)
        {
            return context.Server.MapPath(filepath);
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

        public static string GetUrlResource(this Uri uri)
        {
            return uri.OriginalString;
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
        public static string SendRequest(string url, string data, HttpCookieCollection cookies, string method = "POST", string encoding = "utf-8")
        {
            return SendRequest(url, data, cookies.ToCookieContainer(), method, encoding);
            //Encoding _encoding = Encoding.GetEncoding(encoding);
            //HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
            //Request.CookieContainer = cookies.ToCookieContainer();
            //Request.Method = method;
            //Request.ContentType = "application/x-www-form-urlencoded";
            //Request.AllowAutoRedirect = true;
            //if (!string.IsNullOrEmpty(data))
            //{
            //    byte[] postdata = _encoding.GetBytes(data);
            //    using (Stream newStream = Request.GetRequestStream())
            //    {
            //        newStream.Write(postdata, 0, postdata.Length);
            //    }
            //}
            //using (HttpWebResponse response = (HttpWebResponse)Request.GetResponse())
            //{

            //    using (Stream stream = response.GetResponseStream())
            //    {
            //        using (StreamReader reader = new StreamReader(stream, _encoding, true))
            //        {
            //            return reader.ReadToEnd();
            //        }
            //    }
            //}
        }

        /// <summary>
        /// send HttpWebRequest
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="data">data(k1=v1&k2=v2)</param>
        /// <param name="cookiecontainer">CookieContainer</param>
        /// <param name="method">POST or GET</param>
        /// <param name="encoding">encoding(utf-8, gbk)</param>
        /// <returns></returns>
        public static string SendRequest(string url, string data, CookieContainer cookiecontainer, string method = "POST", string encoding = "utf-8")
        {
            Encoding _encoding = Encoding.GetEncoding(encoding);
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
            Request.CookieContainer = cookiecontainer;
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
