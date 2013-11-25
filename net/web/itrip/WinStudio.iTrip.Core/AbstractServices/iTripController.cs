using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutofacAutoResolver;
using WinStudio.ComResult;
using WinStudio.iTrip.Models;
using WinStudio.iTrip.IBusiness;
using WinStudio.Permission;

namespace WinStudio.iTrip.Core.AbstractServices
{
    public class SessionUser
    {
        public string Id { get; set; }
        public string Account { get; set; }
        public string NickName { get; set; }
        public DateTime LoginTime { get; set; }
    }
    public class iTripController : WinPermissionController
    {
        private static IAutofacResolver _resolver = new AutofacResolver();
        protected T GetService<T>()
        {
            return _resolver.GetService<T>();
        }
        protected ComRet Result() { return new ComRet(); }
        protected ComRet Result(bool ret, string msg = null) { return new ComRet(ret, msg); }
        protected ComRet Result(string err) { return new ComRet(err); }
        protected ComRet Result(object obj) { return new ComRet(obj); }
        protected ComRet Result(int num) { return new ComRet(num); }
        protected ComRet Result(bool ret, string msg, int num, object obj) { return new ComRet(ret, msg, num, obj); }
        //protected ComRet<T> Result(T obj) { return new ComRet<T>(obj); }
        //protected ComRet Result(bool ret, string msg, int num, T obj) { return new ComRet<T>(ret, msg, num, obj); }

        protected ActionResult RedirectToLocal(string url = null)
        {
            if (url != null)//Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public override bool IsClient
        {
            get { return false; }
        }
    }

    public class iTripAccountController : iTripController
    {
        protected void CheckIn(Passport passport)
        {
            Profile profile = GetService<IProfileBusiness>().GetProfile(passport.Account);
            UserInfo user = new UserInfo() { Id = profile.Id, Account = profile.Account, Name = profile.Name };
            WinWebGlobalManager.Reception.Login(HttpContext, user);

            //var key = string.Format("{0}_{1}_{2}", profile.Account, DateTime.Now.ToString("yyyyMMddHHmmssffff"), profile.Id).ToMD5();
            //HttpContext.SaveCookie(Consts.CookieName, key, Consts.CookieDomain, Consts.CookieTimeout);
            //HttpContext.SaveToSession(key, profile);
        }

        protected void CheckOut()
        {
            WinWebGlobalManager.Reception.Logout(HttpContext);
        }
    }
}
