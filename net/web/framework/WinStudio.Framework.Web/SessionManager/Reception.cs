using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WinStudio.Framework.Web.SessionManager
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

        //public static PermissionInfo GetPermissionInfo(HttpContextBase context)
        //{
        //    string token = context.GetToken(Consts.TokenName);
        //    IWinSession session = GetSession(token);
        //    if (session != null)
        //    {
        //        return new PermissionInfo() { UserAccount = session.Account, SysCode = session.Module };
        //    }
        //    return new PermissionInfo();
        //}

        public static string GenerateToken(string account, string module)
        {
            return string.Format("{0}{1}{2}", DateTime.Now.ToString("yyyyMMddHHmmssffff"), account, module).ToMD5();
        }

        public static bool IsExpiredSession(HttpContextBase context)
        {
            string token = context.GetToken(Consts.TokenName);
            if (string.IsNullOrEmpty(token)) return false;
            return Reception.HasSession(token);
        }

        public static void LogOff(HttpContextBase context)
        {
            Expire(context.GetToken(Consts.TokenName));
            context.RemoveCookie(Consts.TokenName);
        }

        public static void FinishLogin(HttpContextBase context, string account, string module)
        {
            var token = GenerateToken(account, module);
            context.SaveToken(account, token);
            AddSession(account, token, module, context.GetAuthority());
            context.SaveToSession(token, GetSession(token));
        }
    }
}
