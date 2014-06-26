using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStuido.Permission.IBusiness;

namespace WinStudio.Permission.Business
{
    public class CookieInfo : ICookieInfo
    {
        private DateTime _lastTime = DateTime.Now;
        private int _timeout = 20;

        public CookieInfo(int timeout = 20) { _timeout = timeout; }

        public string Domain { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public DateTime LastTime { get { return _lastTime; } }


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
    }
}
