using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WinStudio.Permission
{
    public interface IWinPermissionController
    {
        bool IsLogin { get; }

        UserInfo UserInfo { get; }

        string MyId { get; }
        string MyAccount { get; }
        string MyName { get; }
    }

    public abstract class WinPermissionController : Controller, IWinPermissionController
    {
        public abstract bool IsClient { get; }
        public bool IsLogin
        {
            get
            {
                if (IsClient)
                    return WinWebGlobalManager.Guider.IsAuthorized(HttpContext);
                else return WinWebGlobalManager.Reception.IsAuthorized(HttpContext);
            }
        }

        public string MyId
        {
            get
            {
                var user = UserInfo;
                if (user == null) return string.Empty;
                return user.Id;
            }
        }

        public string MyAccount
        {
            get
            {
                var user = UserInfo;
                if (user == null) return string.Empty;
                return user.Account;
            }
        }

        public string MyName
        {
            get
            {
                var user = UserInfo;
                if (user == null) return string.Empty;
                return user.Name;
            }
        }


        public UserInfo UserInfo
        {
            get
            {
                if (IsClient)
                    return WinWebGlobalManager.Guider.GetUserInfo(HttpContext);
                else return WinWebGlobalManager.Reception.GetUserInfo(HttpContext);
            }
        }
    }
}
