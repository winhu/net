using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using WebMatrix.WebData;
using WinStudio.Track.Web.Filters;
using WinStudio.Track.Web.Models;

namespace WinStudio.Track.Web.Controllers
{
    /// <summary>
    /// Simple class that represents a OpenId validated user in the system
    /// </summary>
    public class UserData
    {
        public string Email { get; set; }
        public string FullName { get; set; }

        public override string ToString()
        {
            return String.Format("{0}-{1}", Email, FullName);
        }
    }
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            OpenIdRelyingParty openid = new OpenIdRelyingParty();
            IAuthenticationResponse response = openid.GetResponse();
            if (response != null && response.Status == AuthenticationStatus.Authenticated)
            {
                var claimUntrusted = response.GetUntrustedExtension<ClaimsResponse>();
                var claim = response.GetExtension<ClaimsResponse>();

                UserData userData = null;

                if (claim != null)
                {
                    userData = new UserData();
                    userData.Email = claim.Email;
                    userData.FullName = claim.FullName;
                }

                //fallback to claim untrusted, as some OpenId providers may not
                //provide the trusted ClaimsResponse, so we have to fallback to 
                //trying the untrusted on
                if (claimUntrusted != null && userData == null)
                {
                    userData = new UserData();
                    userData.Email = claimUntrusted.Email;
                    userData.FullName = claimUntrusted.FullName;
                }

                //now store Forms Authorisation cookie 
                IssueAuthTicket(userData, true);

                //store ClaimedIdentifier it in Session 
                //(this would more than likely be something you would store in a database I guess
                Session["ClaimedIdentifierMessage"] = response.ClaimedIdentifier;

                //If we have a ReturnUrl we MUST be using forms authentication, 
                //so redirect to the original ReturnUrl
                if (Session["ReturnUrl"] != null)
                {
                    string url = Session["ReturnUrl"].ToString();
                    return new RedirectResult(url);
                }
                //This should not happen if all controllers have [Authorise] used on them
                else
                    throw new InvalidOperationException("There is no ReturnUrl");
            }
            return View("Login");
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            OpenIdRelyingParty openid = new OpenIdRelyingParty();
            IAuthenticationResponse response = openid.GetResponse();

            IAuthenticationRequest request = openid.CreateRequest(Identifier.Parse(HttpContext.Request.Form["openid_identifier"]));
            var fields = new ClaimsRequest();
            fields.Email = DemandLevel.Require;
            fields.FullName = DemandLevel.Require;
            fields.BirthDate = DemandLevel.Require;
            fields.Gender = DemandLevel.Require;
            fields.Country = DemandLevel.Require;
            fields.Nickname = DemandLevel.Require;
            request.AddExtension(fields);
            return request.RedirectingResponse.AsActionResult();
        }
        #region Private Methods
        /// <summary>
        /// Issue forms authentication ticket for authenticated user, and store the cookie
        /// </summary>
        private void IssueAuthTicket(UserData userData, bool rememberMe)
        {
            FormsAuthenticationTicket ticket =
                new FormsAuthenticationTicket(1, userData.Email,
                    DateTime.Now, DateTime.Now.AddDays(10),
                    rememberMe, userData.ToString());

            string ticketString = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketString);
            if (rememberMe)
                cookie.Expires = DateTime.Now.AddDays(10);

            HttpContext.Response.Cookies.Add(cookie);
        }
        #endregion

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }


        //
        // GET: /Account/ExternalLoginCallback


        #region 帮助程序
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }


        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // 请参见 http://go.microsoft.com/fwlink/?LinkID=177550 以查看
            // 状态代码的完整列表。
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "用户名已存在。请输入其他用户名。";

                case MembershipCreateStatus.DuplicateEmail:
                    return "该电子邮件地址的用户名已存在。请输入其他电子邮件地址。";

                case MembershipCreateStatus.InvalidPassword:
                    return "提供的密码无效。请输入有效的密码值。";

                case MembershipCreateStatus.InvalidEmail:
                    return "提供的电子邮件地址无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidAnswer:
                    return "提供的密码取回答案无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidQuestion:
                    return "提供的密码取回问题无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidUserName:
                    return "提供的用户名无效。请检查该值并重试。";

                case MembershipCreateStatus.ProviderError:
                    return "身份验证提供程序返回了错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

                case MembershipCreateStatus.UserRejected:
                    return "已取消用户创建请求。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

                default:
                    return "发生未知错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";
            }
        }
        #endregion
    }
}
