using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission
{
    [Serializable]
    public class SessionInstance : ISession
    {
        private string _token, _id, _account, _name, _module, _host;
        private DateTime _lastTime = DateTime.Now;
        private int _timeout = 20;

        public SessionInstance(int timeout = 20)
        {
            _timeout = timeout;
        }

        #region 属性
        public string Token
        {
            get
            {
                return _token;
            }
            set
            {
                _token = value;
            }
        }

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Account
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Module
        {
            get
            {
                return _module;
            }
            set
            {
                _module = value;
            }
        }

        public DateTime LastTime
        {
            get
            {
                return _lastTime;
            }
            set
            {
                _lastTime = value;
            }
        }

        public string Host
        {
            get
            {
                return _host;
            }
            set
            {
                _host = value;
            }
        }
        #endregion

        public bool IsExpired
        {
            get { return _timeout <= (DateTime.Now - LastTime).TotalMinutes; }
        }

        public bool IsExpiredAndUpdate
        {
            get
            {
                if (IsExpired) return false;
                else { Reset(); return true; }
            }
        }

        public void Reset()
        {
            _lastTime = DateTime.Now;
        }

        public void Expired()
        {
            _lastTime = DateTime.Now.AddMinutes(0 - _timeout * 10);
        }

        public ISession Clone()
        {
            throw new NotImplementedException();
        }
    }
}
