using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WinStudio.Framework.Authentication
{
    public class Reception
    {
        static Reception()
        {
            sessioncontainer = new WinSessionContainer();
        }
        private static IWinSessionContainer sessioncontainer;
        private static object locker = new object();
        private static void AddSession(string account, string token, string syscode, string sysauth)
        {
            lock (locker)
            {
                sessioncontainer.Add(account, token, syscode, sysauth);
            }
        }

        private static bool HasSession(string token)
        {
            lock (locker)
            {
                return sessioncontainer.Has(token);
            }
        }

        public static IWinSession GetSession(string token)
        {
            lock (locker)
            {
                return sessioncontainer.Pick(token);
            }
        }

        private static void Expire(string token)
        {
            lock (locker)
            {
                sessioncontainer.Expire(token);
            }
        }
        
        public static string GenerateToken(string account, string module)
        {
            return string.Format("{0}{1}{2}", DateTime.Now.ToString("yyyyMMddHHmmssffff"), account, module).ToMD5();
        }

        public static bool IsValidSession(HttpContextBase context)
        {
            string token = context.GetToken(WebConsts.WinTokenName);
            if (string.IsNullOrEmpty(token)) return false;
            return Reception.HasSession(token);
        }

        public static void LogOff(HttpContextBase context)
        {
            Expire(context.GetToken(WebConsts.WinTokenName));
            context.RemoveCookie(WebConsts.WinTokenName);
        }

        public static void FinishLogin(HttpContextBase context, string account, string module)
        {
            var token = GenerateToken(account, module);
            context.SaveToken(account, token);
            AddSession(account, token, module, context.GetAuthority());
            context.SaveToSession(token, GetSession(token));
        }
        public static void FinishLogin(HttpContextBase context, string module)
        {
            string token = context.GetToken();
            if (string.IsNullOrEmpty(token)) return;
            var session = context.GetFromSession<IWinSession>(token);
            if (session == null) return;
            AddSession(session.Account, token, module, context.GetAuthority());
        }
    }
}
