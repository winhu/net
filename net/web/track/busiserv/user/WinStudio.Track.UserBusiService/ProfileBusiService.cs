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
    public class ProfileBusiService : AbstractAuthBusiService<Profile>
    {
        public ComRet GetProfile(string account)
        {
            var cust = Get(c => c.Account == account);
            if (cust == null) return Result("不存在的用户！");
            return Result(cust);
        }
    }
}
