using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Repository;
using WinStudio.ComResult;
using WinStudio.Passport.BusiService.Abstract;
using WinStudio.Passport.Models;

namespace WinStudio.Passport.BusiService.Impl
{
    public class OpenIDBusiService : AbstractPassportBusiService<OpenID>
    {
        public ComRet ValidateUser(string openid)
        {
            return Result(MongoEntity.Count<OpenID>(o => o.OpenTicket == openid) == 1, null);
            //return Result(Exist(s => s.OpenTicket == openid), null);
        }

        public ComRet GetOpenID(string openTicket)
        {
            OpenID openid = MongoEntity.Get<OpenID>(o => o.OpenTicket == openTicket);
            //OpenID openid = Get(o => o.OpenTicket == openTicket);
            if (openid == null) return Result("不存在的OpenID！");
            return Result(openid);
        }

        public void SaveOpenUser(string openid, string type, string openaccount)
        {

            var user = new OpenID() { OpenTicket = openid, Type = type, OpenAccount = openaccount };
            user.Save();
            //Save(user);
        }
    }
}
