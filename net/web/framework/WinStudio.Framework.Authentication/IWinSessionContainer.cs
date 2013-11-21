using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Framework.Authentication
{
    public interface IWinSessionContainer
    {
        void Add(string account, string token, string syscode, string sysauth);

        bool Has(string token);

        void Expire(string token);

        IWinSession Pick(string token);
    }

    public class WinSessionContainer : IWinSessionContainer
    {
        private static List<IWinSession> sessions = new List<IWinSession>();
        public void Add(string account, string token, string syscode, string sysauth)
        {
            RefreshSessions();
            if (sessions.Exists(s => s.Token == token)) return;
            sessions.Add(new WinSession(account, account, token, syscode, sysauth));
        }
        public bool Has(string token)
        {
            RefreshSessions();
            IWinSession session = Pick(token);
            if (session == null) return false;
            return true;
        }
        public void Expire(string token)
        {
            IWinSession session = sessions.SingleOrDefault(s => s.Token == token);
            if (session == null) return;
            session.Expired();
        }
        private void RefreshSessions()
        {
            sessions.RemoveAll(s => s.IsExpired);
        }
        public IWinSession Pick(string token)
        {
            RefreshSessions();
            IWinSession session = sessions.SingleOrDefault(s => s.Token == token);
            if (session == null) return null;
            session.ResetLastTime();
            return session.Clone();
        }
    }
}
