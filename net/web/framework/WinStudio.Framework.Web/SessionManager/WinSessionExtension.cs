using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Framework.Web.SessionManager
{
    public static class WinSessionExtension
    {
        public static void SaveToken(this HttpContextBase context, string account, string token)
        {
            context.SaveCookie(WebConsts.TokenName, token, Consts.TokenDomain, Consts.TokenTimeout);
        }
        public static string GetToken(this HttpContextBase context)
        {
            return context.GetToken(Consts.TokenName);
        }

        public static IWinSession GetWinSession(this HttpContextBase context)
        {
            var token = context.GetToken();
            return context.GetFromSession<IWinSession>(token);
        }
    }
}
