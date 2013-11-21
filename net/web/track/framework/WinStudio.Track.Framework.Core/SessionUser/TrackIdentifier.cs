using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using WinStudio.Track.Models.Authentication;

namespace WinStudio.Track.Framework.Core.SessionUser
{
    public class TrackIdentifier
    {
        public TrackIdentifier(string account, string name, string nickname, bool gender)
        {
            this.account = account;
            this.name = name;
            this.nickname = nickname;
            this.gender = gender;
        }
        public TrackIdentifier(FormsAuthenticationTicket ticket)
        {
            this.ticket = ticket;
            var data = ticket.UserData.Split('-');
            if (data == null || data.Length != 4) throw new Exception("FormsAuthenticationTicket票据格式不正确，请遵循‘account-name-nickname-gender’的格式赋值。");
            this.account = data[0];
            this.name = data[1];
            this.nickname = data[2];
            this.gender = data[3].ToBoolean(false);
        }

        private string account, name, nickname, openid;
        private FormsAuthenticationTicket ticket;
        private bool gender;

        public string Account { get { return account; } }
        public string Name { get { return name; } }
        public bool Gender { get { return gender; } }
        public string NickName { get { return nickname; } }
        public string OpenID { get { return openid; } }

        public FormsAuthenticationTicket Ticket { get { return ticket; } }

        public static TrackIdentifier Create(string account, string name, string nickname, string ticket, bool gender)
        {
            return new TrackIdentifier(account, name, nickname, gender);
        }
        public static TrackIdentifier Create(Profile profile, bool rememberMe = false)
        {
            FormsAuthenticationTicket ticket =
                new FormsAuthenticationTicket(1, profile.NickName,
                    DateTime.Now, DateTime.Now.AddMinutes(20),
                    rememberMe, profile.ToString());
            return new TrackIdentifier(ticket);
        }
        public static TrackIdentifier Create(FormsAuthenticationTicket ticket)
        {
            return new TrackIdentifier(ticket);
        }
    }
}
