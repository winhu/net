using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Business
{
    public class SessionInfo : ISessionInfo
    {
        private DateTime _lasttime = DateTime.MinValue;
        private DateTime _loginTime = DateTime.MinValue;

        private IUserInfo _user;
        public SessionInfo() { }
        public SessionInfo(IUserInfo user, DateTime lastime)
        {
            _user = user;
            this.LastTime = lastime;
        }
        public SessionInfo(IUserInfo user, DateTime lastime, DateTime logintime)
        {
            _user = user;
            this.LastTime = lastime;
            this.LoginTime = logintime;
        }
        public string SecurityKey { get; set; }
        public string DynamicToken { get; set; }

        public DateTime LastTime { get { return _lasttime; } set { _lasttime = value; } }
        public DateTime LoginTime { get { return _loginTime; } set { _loginTime = value; } }

        public IUserInfo UserInfo
        {
            get
            {
                return _user;
            }
        }
    }
}
