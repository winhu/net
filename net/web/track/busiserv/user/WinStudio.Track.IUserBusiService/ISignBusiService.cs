using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetOpenAuth.OpenId.RelyingParty;
using Ninject.Web.Common.Auto;
using WinStudio.ComResult;
using WinStudio.Track.Framework.Repository;

namespace WinStudio.Track.IUserBusiService
{
    [AutoNinject]
    public interface ISignBusiService
    {
        ComRet ValidateUser(string account, string password);

        ComRet ValidateUser(IAuthenticationResponse response);

        ComRet GetProfile(string account, bool isopenauth = false);

        ComRet UpdateUser(IAuthenticationResponse response);
    }
}
