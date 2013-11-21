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
    public class SignKeyBusiService : AbstractPassportBusiService<SignerKey>
    {
        public ComRet ValidateUser(string username, string password)
        {
            SignerKey sk = MongoEntity.Get<SignerKey>(k => k.Account == username);
            //SignerKey sk = Get(s => s.Account == username);
            //if (!sk.Module.Status)
            //    return Result("您所在的模块已经停止认证服务！");
            if (sk == null)
                return Result("不存在的用户！");
            if (!password.IsEqualsMD5(sk.Password))
                return Result("密码不正确！");
            return Result(true, "验证通过！");
        }
    }
}
