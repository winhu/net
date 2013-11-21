using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WinStudio.Framework.Authentication
{
    public class Guider
    {
        public static bool IsAuthorized(HttpContextBase context)
        {
            var token = context.GetToken();
            if (token.HasValue())
            {
                IWinSession session = context.GetFromSession<IWinSession>(token);
                if (session == null)
                {
                    session = OnLoadUserInfo<WinSession>(context);
                    if (session == null) return false;
                    context.SaveToSession(token, session);
                }
                if (!session.IsExpiredAndUpdate)
                {
                    context.UpdateToken();
                    return true;
                }
            }
            return false;
        }
        public static T OnLoadUserInfo<T>(HttpContextBase context) where T : class
        {
            string xml = HttpHelper.SendRequest(WebConsts.WinHandleUpdateInfoAddress, null, context.Request.Cookies);
            return xml.ToObject<T>(Encoding.UTF8);
        }
    }
}
