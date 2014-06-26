using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Permission.Business.Core;
using WinStuido.Permission.IBusiness.Core;

namespace WinStuido.Permission.IBusiness.Reception
{
    public class SessionInfo : UserInfo, ISessionInfo
    {
        public SessionInfo() { }
        public SessionInfo(IUserInfo user)
        {
            this.Account = user.Account;
            this.Email = user.Email;
            this.Id = user.Id;
            this.IP = user.IP;
            this.Name = user.Name;
            this.Origin = user.Origin;
            this.SecurityKey = user.SecurityKey;
            this.RegisterdTime = user.RegisterdTime;
        }
        public string DynamicToken { get; set; }

        public DateTime LastTime { get; set; }
        public DateTime LoginTime { get; set; }


    }
}
