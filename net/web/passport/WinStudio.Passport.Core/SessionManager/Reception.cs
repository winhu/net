using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WinStudio.Passport.Core.SessionManager
{
    public class Reception
    {
        static Reception()
        {
            sessioncontainer = new SysSessionContainer();
        }
        private static ISysSessionContainer sessioncontainer;
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

        private static ISysSession GetSession(string token)
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

        public static PermissionInfo GetPermissionInfo(HttpContextBase context)
        {
            string token = context.GetToken(Consts.TokenName);
            ISysSession session = GetSession(token);
            if (session != null)
            {
                return new PermissionInfo() { UserAccount = session.Account, SysCode = session.SysCode };
            }
            return new PermissionInfo();
        }

        public static bool IsExpiredSession(HttpContextBase context)
        {
            string token = context.GetToken(Consts.TokenName);
            if (string.IsNullOrEmpty(token)) return false;
            return Reception.HasSession(token);
        }

        public static void LogOff(HttpContextBase context)
        {
            context.RemoveCookie(Consts.TokenName);
            Expire(context.GetToken(Consts.TokenName));
        }

        public static void FinishLogin(HttpContextBase context, string account, string token, string syscode)
        {
            context.SaveCookie(Consts.TokenName, token, Consts.TokenDomain, Consts.TokenTimeout);
            AddSession(account, token, syscode, string.Format("{0}://{1}", context.Request.Url.Scheme, context.Request.Url.Authority));
        }
    }
}
