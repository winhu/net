using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using WinStudio.Track.Framework.Core.SessionUser;
using WinStudio.Track.IUserBusiService;
using WinStudio.ComResult;
using WinStudio.Track.Models.Authentication;
using System.Threading;

namespace WinStudio.Track.Web.Controllers
{
    public class AccountController1 : AbstractTrackController
    {
        public AccountController1(ISignBusiService signBusiServ)
        {
            _signBusiServ = signBusiServ;
        }
        ISignBusiService _signBusiServ;
        #region internal login

        public ActionResult Login() { return View(); }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var ret = _signBusiServ.ValidateUser(model.Username, model.Password);
            if (ret.Ret)
            {
                Profile profile = _signBusiServ.GetProfile(model.Username).Instance<Profile>();
                IssueAuthTicket(profile, false);
                return RedirectToLocal();
            }
            return View();
        }

        public class DoStuffViewModel
        {
            public string Text { get; set; }
        }
        public async Task<ViewResult> Stuff()
        {
            return View(new DoStuffViewModel()
                            {
                                Text = await DoStuff("Some other stuff")
                            });
        }

        private async Task<string> DoStuff(string input)
        {
            Thread.Sleep(5000);
            return input;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToLocal();
        }
        #endregion

        public ActionResult OpenAuth()
        { return View(); }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult OpenAuth(string openid_identifier)
        {
            var openid = new OpenIdRelyingParty();

            IAuthenticationRequest request = openid.CreateRequest(Identifier.Parse(openid_identifier), Realm.AutoDetect, new Uri("http://localhost:9000/Account/OpenAuthResponse"));
            //var fetch = new FetchRequest();
            //fetch.Attributes.AddRequired(WellKnownAttributes.Name.FullName);
            //fetch.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
            //fetch.Attributes.AddRequired(WellKnownAttributes.Person.Gender);
            //fetch.Attributes.AddRequired(WellKnownAttributes.Preferences.Language);
            //fetch.Attributes.AddRequired(WellKnownAttributes.BirthDate.WholeBirthDate);
            //fetch.Attributes.AddRequired(WellKnownAttributes.Preferences.TimeZone);
            //fetch.Attributes.AddRequired(WellKnownAttributes.Contact.Phone.Mobile);
            //request.AddExtension(fetch);
            request.AddExtension(new ClaimsRequest
            {
                Email = DemandLevel.Require,
                FullName = DemandLevel.Require,
                Nickname = DemandLevel.Require,
                Country = DemandLevel.Require,
                Gender = DemandLevel.Require,
                Language = DemandLevel.Require,
                BirthDate = DemandLevel.Require,
                TimeZone = DemandLevel.Require,
                PostalCode = DemandLevel.Require,
                PolicyUrl = this.HttpContext.Request.Url
            });
            return request.RedirectingResponse.AsActionResult();
        }
        public ActionResult OpenAuthResponse()
        {
            OpenIdRelyingParty openid = new OpenIdRelyingParty();
            IAuthenticationResponse response = openid.GetResponse();

            if (response != null && response.Status == AuthenticationStatus.Authenticated)
            {
                ComRet ret = _signBusiServ.GetProfile(response.ClaimedIdentifier, true);

                if (!ret.Ret)
                    ret = _signBusiServ.ValidateUser(response);

                if (ret.Ret)
                {
                    IssueAuthTicket(ret.Instance<Profile>(), false);
                    //store ClaimedIdentifier it in Session 
                    //(this would more than likely be something you would store in a database I guess
                    Session["ClaimedIdentifierMessage"] = response.ClaimedIdentifier;

                    return RedirectToLocal();
                }
            }
            return RedirectToLocal();
        }

        #region Private Methods
        /// <summary>
        /// Issue forms authentication ticket for authenticated user, and store the cookie
        /// </summary>
        private void IssueAuthTicket(Profile profile, bool rememberMe = true)
        {
            TrackIdentifier identifier = TrackIdentifier.Create(profile, rememberMe);
            FormsAuthentication.SetAuthCookie(identifier.NickName, false);
            string ticketString = FormsAuthentication.Encrypt(identifier.Ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketString);
            if (rememberMe)
                cookie.Expires = DateTime.Now.AddDays(10);

            HttpContext.Response.Cookies.Add(cookie);
        }
        #endregion
    }
}
