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
    public class SignKeyBusiService : AbstractAuthBusiService<SignerKey>
    {
        public ComRet ValidateUser(string username, string password)
        {
            SignerKey sk = Get(s => s.Account == username);
            if (sk == null)
                return Result("不存在的用户！");
            if (sk.Password != password.ToMD5())
                return Result("密码不正确！");
            return Result(true, "验证通过！");
        }
    }
}
