using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Framework.Authentication
{
    public interface IWinSession
    {
        string Account { get; }
        string NickName { get; }
        string Token { get; }
        string Host { get; }
        string Module { get; }

        DateTime LastTime { get; }

        bool IsExpired { get; }
        bool IsExpiredAndUpdate { get; }

        void ResetLastTime();
        void Expired();
        IWinSession Clone();
    }

    public class WinSession : IWinSession, IDisposable
    {
        public WinSession() { }
        public WinSession(string account, string token) : this(account, account, token, null, null) { }
        public WinSession(string account, string nickname, string token, string module, string host)
        {
            this.account = account;
            this.token = token;
            this.host = host;
            this.module = module;
            this.nickname = nickname.HasValue() ? nickname : account;
        }
        private string account, nickname, token, module, host;

        public string Account { get { return account; } set { account = value; } }
        public string NickName { get { return nickname; } set { nickname = value; } }
        public string Token { get { return token; } set { token = value; } }
        public string Host { get { return host; } set { host = value; } }
        public string Module { get { return module; } set { module = value; } }
        public bool IsExpired
        {
            get
            {
                return lasttime.AddMinutes(WebConsts.WinTokenTimeout) < DateTime.Now;
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

        public IWinSession Clone()
        {
            return new WinSession(this.Account, this.NickName, this.Token, this.Module, this.Host);
        }


        public bool IsExpiredAndUpdate
        {
            get
            {
                var working = IsExpired;
                if (working) ResetLastTime();
                else Dispose();
                return working;
            }
        }
        public void Dispose()
        {
            
        }
    }
}
