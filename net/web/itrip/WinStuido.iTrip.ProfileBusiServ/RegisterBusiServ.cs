using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.iTrip.Core.AbstractServices;
using WinStudio.iTrip.IBusiness;
using WinStudio.ComResult;
using WinStudio.iTrip.Models;
using MongoDB.Repository;
using System.ComponentModel.Composition;

namespace WinStuido.iTrip.ProfileBusiServ
{
    [Export(typeof(IRegisterProfile))]
    public class RegisterBusiServ : iTripBusiServ, IRegisterProfile
    {
        public ComRet RegisterPassport(Passport passport)
        {
            if (!passport.IsValid()) return Result(passport.GetMsg());
            if (MongoEntity.Exists<Passport>(p => p.Account == passport.Account))
            {
                return Result("账号已存在");
            }
            passport.Save();
            return Result();
        }

        public ComRet RegisterProfile(Profile profile)
        {
            if (!profile.IsValid()) return Result(profile.GetMsg());
            if (MongoEntity.Exists<Profile>(p => p.Account == profile.Account))
            {
                return Result("账号已存在");
            }
            profile.Save();
            return Result();
        }


        public ComRet Login(Passport passport)
        {
            if (MongoEntity.Exists<Passport>(p => p.Account == passport.Account && p.Password == passport.Password)) return Result();
            return Result("用户名或密码错误");
        }
    }
}
