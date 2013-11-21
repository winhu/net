using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.Track.Framework.Core;
using WinStudio.Track.Models.Authentication;

namespace WinStudio.Track.UserBusiService
{
    public class OpenIDBusiService : AbstractAuthBusiService<OpenID>
    {
        public ComRet ValidateUser(string openid)
        {
            return Result(Exist(s => s.OpenTicket == openid), null);
        }

        public ComRet GetOpenID(string openTicket)
        {
            OpenID openid = Get(o => o.OpenTicket == openTicket);
            if (openid == null) return Result("不存在的OpenID！");
            return Result(openid);
        }

        public void SaveOpenUser(string openid, string type, string openaccount)
        {
            var user = new OpenID() { OpenTicket = openid, Type = type, OpenAccount = openaccount };
            Save(user);
        }
    }
}
