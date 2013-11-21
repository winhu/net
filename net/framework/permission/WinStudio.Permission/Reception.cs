using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WinStudio.Permission
{
    public interface IReception
    {
        string Login(HttpContextBase context, UserInfo user);
        void Logout(HttpContextBase context);
        bool IsAuthorized(HttpContextBase context);

        void SaveCookie(HttpContextBase context, string token);
        void RemoveCookie(HttpContextBase context);

        int GetLoginCount();
        UserInfo GetUserInfo(HttpContextBase context);
        List<UserInfo> GetLoginUserList();
    }
    public class Reception : IReception
    {
        public Reception(int timetout)
        {
            _sessionManager = new SessionManager(timetout);
        }
        private ISessionManager _sessionManager;
        public string Login(HttpContextBase context, UserInfo user)
        {
            var token = context.GetToken();
            _sessionManager.Expire(token);
            token = _sessionManager.Add(user.Id, user.Account, user.Name, user.Module, user.Host);
            SaveCookie(context, token);
            return token;
        }

        public void Logout(HttpContextBase context)
        {
            var token = context.GetToken();
            _sessionManager.Expire(token);
            RemoveCookie(context);
        }

        public bool IsAuthorized(HttpContextBase context)
        {
            var token = context.GetToken();
            return !_sessionManager.IsExpired(token);
        }


        public int GetLoginCount()
        {
            return _sessionManager.Count;
        }


        public List<UserInfo> GetLoginUserList()
        {
            return _sessionManager.GetList();
        }


        public virtual void SaveCookie(HttpContextBase context, string token)
        {
            context.SaveCookie(WinWebGlobalManager.Config.WinTokenName, token, WinWebGlobalManager.Config.WinTokenDomain, WinWebGlobalManager.Config.WinTokenTimeout);
        }

        public virtual void RemoveCookie(HttpContextBase context)
        {
            context.RemoveCookie(WinWebGlobalManager.Config.WinTokenName);
        }


        public UserInfo GetUserInfo(HttpContextBase context)
        {
            ISession session = _sessionManager.Get(context.GetToken());
            if (session == null) return null;
            return new UserInfo()
            {
                Token = session.Token,
                Account = session.Account,
                Id = session.Id,
                Name = session.Name,
                Module = session.Module,
                Host = session.Host,
                LastTime = session.LastTime
            };
        }
    }
}
