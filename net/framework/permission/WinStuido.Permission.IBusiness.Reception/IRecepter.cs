using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WinStuido.Permission.IBusiness.Core;

namespace WinStuido.Permission.IBusiness.Reception
{
    public interface IRecepter
    {
        string Login(HttpContextBase context, IUserInfo user);
        void Logout(HttpContextBase context);
        bool IsAuthorized(HttpContextBase context);

        void SaveCookie(HttpContextBase context, string token);
        void RemoveCookie(HttpContextBase context);

        int GetLoginCount();
        IUserInfo GetUserInfo(string token);
    }

    public class Recepter : IRecepter
    {
        private ISessionManager _sessionManager;
        public Recepter(int timetout)
        {
            _sessionManager = new SessionManager(timetout);
        }
        public string Login(HttpContextBase context, IUserInfo user)
        {
            var token = context.GetToken();
            _sessionManager.Expire(token);
            token = _sessionManager.Add(user.Id, user.Account, user.Name, user.Module, user.Host);
            context.SaveToSession(token, GetUserInfo(token));
            SaveCookie(context, token);
            return token;
        }

        public void Logout(HttpContextBase context)
        {
            throw new NotImplementedException();
        }

        public bool IsAuthorized(HttpContextBase context)
        {
            throw new NotImplementedException();
        }

        public void SaveCookie(HttpContextBase context, string token)
        {
            throw new NotImplementedException();
        }

        public void RemoveCookie(HttpContextBase context)
        {
            throw new NotImplementedException();
        }

        public int GetLoginCount()
        {
            throw new NotImplementedException();
        }

        public IUserInfo GetUserInfo(string token)
        {
            throw new NotImplementedException();
        }
    }
}
