using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Passport.BusiService.Abstract;
using WinStudio.Passport.BusiService.Intefaces;
using WinStudio.Passport.Models;

namespace WinStudio.Passport.BusiService.Impl
{
    [Export(typeof(IUserSignHisTrace))]
    public class HisUserSignBusiService : AbstractPassportBusiService<UserSignHistory>, IUserSignHisTrace
    {
        public void TraceHis(string account, string ip)
        {
            if (string.IsNullOrEmpty(account)) return;
            //Save(new UserSignHistory() { Account = account, IP = ip });
            new UserSignHistory() { Account = account, IP = ip }.Save();
        }
    }
}
