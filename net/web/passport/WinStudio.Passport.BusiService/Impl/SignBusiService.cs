using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using MongoDB.Repository;
using WinStudio.ComResult;
using WinStudio.Passport.BusiService.Intefaces;
using WinStudio.Passport.BusiService.Interfaces;
using WinStudio.Passport.Models;
using WinStudio.Security;

namespace WinStudio.Passport.BusiService.Impl
{
    [Export(typeof(ISignBusiService))]
    public class SignBusiService : ISignBusiService
    {
        SignKeyBusiService bsSignKey;
        OpenIDBusiService bsOpenID;
        ProfileBusiService bsProfile;

        IUserSignHisTrace bsUserSign;
        IModuleSignHisTrace bsModuleSign;
        public SignBusiService(IUserSignHisTrace usersign, IModuleSignHisTrace modulesign)
        {
            bsUserSign = usersign;
            bsModuleSign = modulesign;
        }
        public SignBusiService() { }

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
            bsProfile.SaveProfile(profile);

            OpenID openid = new OpenID().RetraceFrom(response.ClaimedIdentifier, claim.MailAddress.ToString(), profile.Account);

            //bsOpenID = new OpenIDBusiService();
            //bsOpenID.Save(openid);
            openid.Save();

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


        public ComRet TraceSign(string account, string module, string ip)
        {
            throw new NotImplementedException();
        }


        public ComRet RegisterUser(string account, string password, string email)
        {
            if (MongoEntity.Exists<SignerKey>(p => p.Account == account))
                return new ComRet("账号已存在，请更换其他的用户名！");
            if (MongoEntity.Exists<SignerKey>(p => p.Email == email))
                return new ComRet("Email已存在，请更换其他的Email！");
            var key = new SignerKey() { Account = account, Email = email, Password = Signer.Create(AlgorithmName.md5).Sign(password) };
            key.Save();
            return new ComRet(true, account);
        }
    }
}
