using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WinStudio.Permission
{
    public interface IGuider
    {
        bool IsAuthorized(HttpContextBase context);
        UserInfo OnLoadUserInfo(HttpContextBase context);

        bool OnValidateRequest(HttpContextBase context);

        UserInfo GetUserInfo(HttpContextBase context);

    }
    public class Guider : IGuider
    {
        public bool IsAuthorized(HttpContextBase context)
        {
            var token = context.GetToken();
            if (string.IsNullOrEmpty(token)) return false;

            return OnValidateRequest(context);
        }

        public virtual UserInfo OnLoadUserInfo(HttpContextBase context)
        {
            string xml = HttpHelper.SendRequest(WinWebGlobalManager.Config.WinHandleUpdateInfoAddress, null, context.Request.Cookies);
            return xml.ToObject<UserInfo>(Encoding.UTF8);
        }

        public virtual bool OnValidateRequest(HttpContextBase context)
        {
            string ret = HttpHelper.SendRequest(WinWebGlobalManager.Config.WinHandleValidatationAddress, null, context.Request.Cookies);
            return Convert.ToBoolean(ret);
        }

        public UserInfo GetUserInfo(HttpContextBase context)
        {
            var token = context.GetToken();
            if (string.IsNullOrEmpty(token)) return null;
            UserInfo user = context.GetFromSession<UserInfo>(token);
            if (null == user)
            {
                user = OnLoadUserInfo(context);
                if (user != null)
                    context.SaveToSession(user.Token, user);
            }
            return user;
        }
    }
}
