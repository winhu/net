using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetOpenAuth.OpenId.RelyingParty;
using WinStudio.ComResult;

namespace WinStudio.Passport.BusiService.Interfaces
{
    public interface ISignBusiService
    {
        ComRet ValidateUser(string account, string password);

        ComRet ValidateUser(IAuthenticationResponse response);

        ComRet GetProfile(string account, bool isopenauth = false);

        ComRet UpdateUser(IAuthenticationResponse response);

        ComRet TraceSign(string account, string module, string ip);

        ComRet RegisterUser(string account, string password, string email);
    }
}
