using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Framework.Authentication;

namespace WinStudio.Framework.Authentication
{
    public static class WinSessionExtension
    {
        public static void SaveToken(this HttpContextBase context, string account, string token)
        {
            context.SaveCookie(WebConsts.WinTokenName, token, WebConsts.WinTokenDomain, WebConsts.WinTokenTimeout);
        }
        public static string GetToken(this HttpContextBase context)
        {
            return context.GetToken(WebConsts.WinTokenName);
        }

        public static IWinSession GetWinSession(this HttpContextBase context)
        {
            var token = context.GetToken();
            return context.GetFromSession<IWinSession>(token);
        }

        public static void UpdateToken(this HttpContextBase context)
        {
            context.UpdateCookieExpires(WebConsts.WinTokenName, WebConsts.WinTokenDomain, WebConsts.WinTokenTimeout);
        }

    }
}
namespace System.Web
{
    public static class WinHttpExtensions
    {
        public static bool IsLogin(this HttpContext context)
        {
            return null != context.ToBase().GetWinSession();
        }

        public static string MyAccount(this HttpContext context)
        {
            IWinSession ws = context.ToBase().GetWinSession();
            if (ws == null) return null;
            return ws.Account;
        }
        public static string MyNickName(this HttpContext context)
        {
            IWinSession ws = context.ToBase().GetWinSession();
            if (ws == null) return null;
            return ws.NickName;
        }
        public static DateTime MyLastTime(this HttpContext context)
        {
            IWinSession ws = context.ToBase().GetWinSession();
            if (ws == null) return DateTime.MinValue;
            return ws.LastTime;
        }
    }
}
