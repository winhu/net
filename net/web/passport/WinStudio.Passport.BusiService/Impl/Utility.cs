using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using WinStudio.Passport.Models;

namespace WinStudio.Passport.BusiService.Impl
{
    public static class Utility
    {
        public static Profile ToProfile(this ClaimsResponse response)
        {
            if (response == null) return null;
            Profile profile = new Profile();
            profile.Gender = (WinStudio.Framework.EFModels.Gender)(response.Gender ?? Gender.Male);
            profile.Account = response.MailAddress.ToString();
            profile.Country = response.Country;
            profile.BirthDate = response.BirthDate;
            profile.Name = response.FullName.HasValue() ? response.FullName : profile.Account;
            profile.NickName = response.Nickname.HasValue() ? response.Nickname : profile.Account;
            profile.Add(new Contact() { Type = ContactType.Email, Content = response.Email });
            return profile;
        }
        public static OpenID RetraceFrom(this OpenID openid, string Identifier, string openaccount, string account)
        {
            openid.OpenTicket = Identifier;
            openid.OpenAccount = openaccount;
            openid.Account = account;
            openid.Type = "eMail";
            return openid;
        }
    }
}
