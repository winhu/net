using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Passport.Core.SessionManager
{
    public interface ISysSessionContainer
    {
        void Add(string account, string token, string syscode, string sysauth);

        bool Has(string token);

        void Expire(string token);

        ISysSession Pick(string token);
    }

    public class SysSessionContainer : ISysSessionContainer
    {
        private static List<ISysSession> sessions = new List<ISysSession>();
        public void Add(string account, string token, string syscode, string sysauth)
        {
            RefreshSessions();
            if (sessions.Exists(s => s.Token == token)) return;
            sessions.Add(new SysSession(account, token, syscode, sysauth));
        }
        public bool Has(string token)
        {
            RefreshSessions();
            ISysSession session = Pick(token);
            if (session == null) return false;
            return true;
        }
        public void Expire(string token)
        {
            ISysSession session = sessions.SingleOrDefault(s => s.Token == token);
            if (session == null) return;
            session.Expired();
        }
        private void RefreshSessions()
        {
            sessions.RemoveAll(s => s.IsExpired);
        }
        public ISysSession Pick(string token)
        {
            RefreshSessions();
            ISysSession session = sessions.SingleOrDefault(s => s.Token == token);
            if (session == null) return null;
            session.ResetLastTime();
            return session.Clone();
        }
    }
}
