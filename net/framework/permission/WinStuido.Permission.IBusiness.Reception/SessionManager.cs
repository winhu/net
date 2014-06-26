using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStuido.Permission.IBusiness.Core;

namespace WinStuido.Permission.IBusiness.Reception
{
    public class SessionManager : ISessionManager
    {
        private int _timeout = 20;
        public SessionManager(int timeout = 20)
        {
            _timeout = timeout;
        }
        private List<ISessionInfo> _sessions = new List<ISessionInfo>();

        private ISessionInfo GetSession(string securitykey)
        {
            return _sessions.SingleOrDefault(s => s.SecurityKey == securitykey && s.LastTime > DateTime.Now.AddMinutes(_timeout));
        }
        private void Refresh()
        {
            LockerManager.State.EnterWriteLock();
            _sessions.RemoveAll(s => s.LastTime > DateTime.Now.AddMinutes(_timeout));
            LockerManager.State.ExitWriteLock();
        }

        public void Expire(string securitykey)
        {
            ISessionInfo session = GetSession(securitykey);
            if (session == null) return;
            session.LastTime = DateTime.Now.AddMinutes(-10 * _timeout);
        }

        public bool IsExpired(string securitykey, string dynamictoken)
        {
            ISessionInfo session = GetSession(securitykey);
            if (session == null) return true;
            return session.DynamicToken != dynamictoken;
        }

        public bool IsLegal(string securitykey)
        {
            return _sessions.Exists(s => s.SecurityKey == securitykey && s.LastTime > DateTime.Now.AddMinutes(_timeout));
        }


        public IUserInfo[] GetAllUser()
        {
            int counter = CountAll();
            IUserInfo[] users = new IUserInfo[counter];
            _sessions.ToList<IUserInfo>().CopyTo(users);
            return users;
        }

        public IUserInfo[] GetValidUsers()
        {
            int counter = CountValid();
            IUserInfo[] users = new IUserInfo[counter];
            _sessions.Where(s => !string.IsNullOrEmpty(s.DynamicToken)).ToList<IUserInfo>().CopyTo(users);
            return users;
        }

        public IUserInfo[] GetLegalUsers()
        {
            int counter = CountLegal();
            IUserInfo[] users = new IUserInfo[counter];
            _sessions.Where(s => string.IsNullOrEmpty(s.DynamicToken)).ToList<IUserInfo>().CopyTo(users);
            return users;
        }

        public int CountAll()
        {
            Refresh();
            return _sessions.Count();
        }

        public int CountLegal()
        {
            Refresh();
            return _sessions.Count(s => string.IsNullOrEmpty(s.DynamicToken));
        }

        public int CountValid()
        {
            Refresh();
            return _sessions.Count(s => !string.IsNullOrEmpty(s.DynamicToken));
        }

        public void Add(IUserInfo user, DateTime lasttime)
        {
            Add(user, lasttime, DateTime.MinValue, null);
        }

        public void Add(IUserInfo user, DateTime lasttime, DateTime logintime, string securityToken)
        {
            ISessionInfo session = new SessionInfo(user);
            session.LastTime = lasttime;
            if (logintime != DateTime.MinValue)
                session.LoginTime = logintime;
            session.DynamicToken = securityToken;
            _sessions.Add(session);
        }
    }
}
