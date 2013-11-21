using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Permission.Common.Framework.SystemSession
{
    public interface ISysSession
    {
        string Account { get; }

        string Token { get; }
        string SysAuth { get; }
        string SysCode { get; }

        DateTime LastTime { get; }

        bool IsExpired { get; }

        void ResetLastTime();
        void Expired();
        ISysSession Clone();
    }

    public class SysSession : ISysSession
    {
        public SysSession(string account, string token, string syscode, string sysauth)
        {
            this.account = account;
            this.token = token;
            this.sysauth = sysauth;
            this.syscode = syscode;
        }
        private string account;
        private string token;
        private string syscode;
        private string sysauth;
        public string Account { get { return account; } }
        public string Token { get { return token; } }
        public string SysAuth { get { return sysauth; } }
        public string SysCode { get { return syscode; } }
        public bool IsExpired
        {
            get
            {
                return lasttime.AddMinutes(20) < DateTime.Now;
            }
        }

        public DateTime LastTime { get { return lasttime; } }
        private DateTime lasttime = DateTime.Now;


        public void ResetLastTime()
        {
            lasttime = DateTime.Now; ;
        }
        public void Expired()
        {
            lasttime = DateTime.Now.AddYears(-1);
        }

        public ISysSession Clone()
        {
            return new SysSession(this.Account, this.Token, this.SysCode, this.SysAuth);
        }
    }
}
