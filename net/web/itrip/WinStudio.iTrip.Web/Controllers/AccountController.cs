using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using WinStudio.iTrip.Core.AbstractServices;
using WinStudio.iTrip.IBusiness;
using WinStudio.iTrip.ICommonBusiness;
using WinStudio.iTrip.Models;
using WinStudio.iTrip.Web.Filters;
using WinStudio.iTrip.Web.Models;

namespace WinStudio.iTrip.Web.Controllers
{
    [Authorize]
    //[InitializeSimpleMembership]
    public class AccountController : iTripAccountController
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string next)
        {
            ViewBag.next = next;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string next)
        {
            if (ModelState.IsValid)//&& WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                var ret = GetService<IRegisterProfile>().Login(model.ToPassport());
                if (ret.Ret)
                {
                    CheckIn(model.ToPassport());
                    return RedirectToLocal(next.FromBase64String());
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            ModelState.AddModelError("", "提供的用户名或密码不正确。");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            CheckOut();

            return RedirectToLocal();
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // 尝试注册用户
                try
                {
                    var tp = RegisterProfile(model.ToProfile());
                    var tc = RegisterCustomer(model.ToCustomer());
                    var ret = GetService<IRegisterProfile>().RegisterPassport(model.ToPassport());
                    if (ret.Ret)
                    {
                        await Task.WhenAll(tp, tc);
                        if (tp.Result && tc.Result)
                        {
                            return RedirectToLocal();
                        }
                        else
                        {
                            return RedirectToLocal("/Account/Profile/" + model.UserName);
                        }
                    }
                    else
                        ModelState.AddModelError("", ret.LastMsg);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        private async Task<bool> RegisterProfile(Profile profile)
        {
            return GetService<IRegisterProfile>().RegisterProfile(profile).Ret;
        }
        private async Task<bool> RegisterCustomer(Customer customer)
        {
            return GetService<ICustomerBusiness>().RegisterCustomer(customer).Ret;
        }

    }
}
