using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using Ninject.Web.Common.Auto;
using WinStudio.ComResult;
using WinStudio.Track.IUserBusiService;
using WinStudio.Track.Models.Authentication;

namespace WinStudio.Track.UserBusiService
{
    [AutoNinject]
    public class SignBusiService : ISignBusiService
    {
        SignKeyBusiService bsSignKey;
        OpenIDBusiService bsOpenID;
        ProfileBusiService bsProfile;

        public ComRet ValidateUser(string account, string password)
        {
            bsSignKey = new SignKeyBusiService();
            return bsSignKey.ValidateUser(account, password);
        }

        public ComRet ValidateUser(IAuthenticationResponse response)
        {
            bsOpenID = new OpenIDBusiService();
            ComRet ret = bsOpenID.ValidateUser(response.ClaimedIdentifier);
            if (ret.Ret) return ret;

            var claim = response.GetExtension<ClaimsResponse>();
            var profile = claim.ToProfile();
            if (profile == null) throw new Exception("OpenID认证未成功！");
            bsProfile = new ProfileBusiService();
            bsProfile.Save(profile);

            OpenID openid = new OpenID().RetraceFrom(response.ClaimedIdentifier, claim.MailAddress.ToString(), profile.Account);

            bsOpenID = new OpenIDBusiService();
            bsOpenID.Save(openid);
            
            ret.AddInstance(profile);
            return ret.AddMsg("认证成功！", true);
        }

        public ComRet GetProfile(string account, bool isopenauth = false)
        {
            if (!isopenauth)
            {
                bsProfile = new ProfileBusiService();
                return bsProfile.GetProfile(account);
            }
            else
            {
                bsOpenID = new OpenIDBusiService();
                ComRet ret = bsOpenID.GetOpenID(account);
                if (!ret.Ret) return ret;
                bsProfile = new ProfileBusiService();
                return bsProfile.GetProfile(ret.Instance<OpenID>().Account);
            }
        }



        public ComRet UpdateUser(IAuthenticationResponse response)
        {
            return null;
            //Profile profile = new Profile() { Account = openaccount, Name = name, NickName = nickname, Gender = gender };
            //bsProfile = new ProfileBusiService();
            //bsProfile.Save(profile);

            //OpenID openid = new OpenID() { Account = openaccount, OpenAccount = openaccount, OpenTicket = ticket };
        }
    }
}
