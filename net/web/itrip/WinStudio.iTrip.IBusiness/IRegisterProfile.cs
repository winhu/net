using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.ComResult;
using WinStudio.iTrip.Models;

namespace WinStudio.iTrip.IBusiness
{
    public interface IRegisterProfile
    {
        ComRet RegisterPassport(Passport passport);
        ComRet RegisterProfile(Profile profile);
        ComRet Login(Passport passport);
    }
}
