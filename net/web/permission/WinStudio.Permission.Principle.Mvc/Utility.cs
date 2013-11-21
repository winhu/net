using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WinStudio.Permission.Principle.Mvc
{
    public class PermissionMvcUtility
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
        internal static string SendRequest(string url, string data, HttpCookieCollection cookies, FormMethod method, Encoding encoder, PermissionConfig config)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("Account", config.Account);
            nvc.Add("SysCode", config.SysCode);
            nvc.Add("SysAuth", config.SysAuth);
            nvc.Add("Token", config.Token);

            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
            Request.CookieContainer = cookies.ToCookieContainer();
            Request.Method = method.ToString();
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.AllowAutoRedirect = true;
            Request.Headers.Add(nvc);
            //if (!string.IsNullOrEmpty(data))
            //{
            //    byte[] postdata = encoder.GetBytes(data);
            //    using (Stream newStream = Request.GetRequestStream())
            //    {
            //        newStream.Write(postdata, 0, postdata.Length);
            //    }
            //}
            using (HttpWebResponse response = (HttpWebResponse)Request.GetResponse())
            {

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, encoder, true))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}
namespace System.Web.Mvc
{
    public static class PermissionExtensions
    {
        internal static bool IsActionNeedValidated(this AuthorizationContext context)
        {
            return context.ActionDescriptor.GetCustomAttributes(typeof(NeedValidPermissionAttribute), true).Length > 0;
        }
        internal static string GetAbsUrl(this Uri uri)
        {
            return string.Format("{0}://{1}{2}", uri.Scheme, uri.Authority, uri.AbsolutePath);
        }
    }
}
