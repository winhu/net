using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission
{
    public class SessionManager : ISessionManager
    {
        private int _timeout = 20;
        private List<ISession> _sessionList;
        private ICommonService _service;
        public SessionManager(ICommonService service, int timeout)
        {
            _timeout = timeout;
            _service = service;
            _sessionList = new List<ISession>();
        }
        public SessionManager(int timeout)
            : this(new CommonService(), timeout)
        { }

        private void Refresh()
        {
            LockerManager.State.EnterWriteLock();
            _sessionList.RemoveAll(s => s.IsExpired);
            LockerManager.State.ExitWriteLock();
        }

        public void Expire(string token)
        {
            ISession session = Get(token);
            if (session != null) session.Expired();
        }

        public ISession Get(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;
            Refresh();
            return _sessionList.SingleOrDefault(s => s.Token == token);
        }

        public bool IsExpired(string token)
        {
            if (string.IsNullOrEmpty(token)) return true;
            Refresh();
            return !_sessionList.Exists(s => s.Token == token);
        }

        public List<UserInfo> GetList()
        {
            Refresh();
            return _sessionList.Select(i => new UserInfo() { Token = i.Token, Id = i.Id, Account = i.Account, Name = i.Name, Module = i.Module, Host = i.Host, LastTime = i.LastTime }).ToList();
        }

        public int Count
        {
            get
            {
                Refresh();
                return _sessionList.Count;
            }
        }


        private void Add(ISession session)
        {
            if (!IsExpired(session.Token)) return;
            _sessionList.Add(session);
        }
        public string Add(string id, string account, string name, string module, string host)
        {
            var session = new SessionInstance(_timeout) { Id = id, Account = account, Name = name, Module = module, Host = host, Token = _service.GenerateToken(account) };
            Add(session);
            return session.Token;
        }
        public string Add(string id, string account, string name)
        {
            return Add(id, account, name, string.Empty, string.Empty);
        }
        public string Add(string id, string account)
        {
            return Add(id, account, string.Empty, string.Empty, string.Empty);
        }
    }
}
