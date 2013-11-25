using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using WinStudio.Permission;

namespace System.Web
{
    public static class Utility
    {
        public static void SaveToken(this HttpContextBase context, string account, string token)
        {
            context.SaveCookie(WinWebGlobalManager.Config.WinTokenName, token, WinWebGlobalManager.Config.WinTokenDomain, WinWebGlobalManager.Config.WinTokenTimeout);
        }
        public static string GetToken(this HttpContextBase context)
        {
            return context.GetToken(WinWebGlobalManager.Config.WinTokenName);
        }
        internal static UserInfo GetUserInfo(this HttpContextBase context)
        {
            var token = context.GetToken();
            if (string.IsNullOrEmpty(token)) return null;
            var user = context.GetFromSession<UserInfo>(token);
            return user;
        }
        internal static bool IsLogin(this HttpContextBase context)
        {
            return null != context.GetUserInfo();
        }
        internal static string GetAccount(this HttpContextBase context)
        {
            var user = context.GetUserInfo();
            if (user == null) return string.Empty;
            return user.Account;
        }
        internal static string GetName(this HttpContextBase context)
        {
            var user = context.GetUserInfo();
            if (user == null) return string.Empty;
            return user.Name;
        }
        internal static string GetId(this HttpContextBase context)
        {
            var user = context.GetUserInfo();
            if (user == null) return string.Empty;
            return user.Id;
        }
        public static bool IsLogin(this HttpContext context)
        {
            return context.ToBase().IsLogin();
        }
        public static string MyAccount(this HttpContext context)
        {
            return context.ToBase().GetAccount();
        }
        public static string MyName(this HttpContext context)
        {
            return context.ToBase().GetName();
        }
        public static string MyId(this HttpContext context)
        {
            return context.ToBase().GetId();
        }

        /// <summary>
        /// convert xml string to object
        /// </summary>
        /// <typeparam name="T">objecct</typeparam>
        /// <param name="s">xml string</param>
        /// <param name="encoding">encoding</param>
        /// <returns></returns>
        public static T ToObject<T>(this string s, Encoding encoding) where T : class
        {
            if (!string.IsNullOrEmpty(s) || !typeof(T).IsSerializable) return default(T);
            byte[] byteArray = encoding.GetBytes(s);
            MemoryStream stream = new MemoryStream(byteArray);
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
            return xs.Deserialize(stream) as T;
        }

        /// <summary>
        /// Serialize object to xml string
        /// </summary>
        /// <param name="o">object</param>
        /// <param encoding="o">encoding</param>
        /// <returns></returns>
        public static string ToXml(this object o, string encoding = "utf-8")
        {
            if (o != null && o.GetType().IsSerializable)
            {
                try
                {
                    XmlSerializer x = new XmlSerializer(o.GetType());
                    MemoryStream stream = new MemoryStream();
                    Encoding _encoding = Encoding.GetEncoding(encoding);
                    XmlTextWriter writer = new XmlTextWriter(stream, _encoding);
                    x.Serialize(writer, o);
                    byte[] utf8EncodedData = stream.GetBuffer();
                    return _encoding.GetString(utf8EncodedData, 0, (int)stream.Length);
                }
                catch (Exception e) { throw e; }
            }
            return string.Empty;
        }
    }

    public class WinWebGlobalManager
    {
        private static IReception _reception;
        private static IGuider _guider;
        private static PermissionConfiguration _config;
        public static PermissionConfiguration Config
        {
            get
            {
                return _config;
            }
        }
        public static IReception Reception
        {
            get
            {
                if (_reception == null)
                {
                    LockerManager.State.EnterReadLock();
                    if (_reception == null)
                        _reception = new Reception(_config.WinTokenTimeout);
                    LockerManager.State.ExitReadLock();
                }
                return _reception;
            }
        }
        public static IGuider Guider
        {
            get
            {
                if (_guider == null)
                {
                    LockerManager.State.EnterReadLock();
                    if (_guider == null)
                        _guider = new Guider();
                    LockerManager.State.ExitReadLock();
                }
                return _guider;
            }
        }

        public static void Initialize(PermissionConfiguration config, IReception reception, IGuider guider)
        {
            _config = config;
            _guider = guider;
            _reception = reception;
        }
    }
}
