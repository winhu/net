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
    public class ProfileBusiService : AbstractPassportBusiService<Profile>
    {
        public ComRet GetProfile(string account)
        {
            var cust = MongoEntity.Get<Profile>(p => p.Account == account);
            //var cust = Get(c => c.Account == account);
            if (cust == null) return Result("不存在的用户！");
            return Result(cust);
        }

        public ComRet SaveProfile(Profile profile)
        {
            if (!MongoEntity.Exists<Profile>(p => p.Account == profile.Account))
                profile.Save();
            return Result("该账号已经被注册！");
        }
    }
}
