using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WinStudio.ComResult;
using WinStudio.Framework.Authentication;
using WinStudio.Permission.BusiServices.Impl;
using WinStudio.Permission.BusiServices.Interfaces;
using WinStudio.Permission.Common.Framework;
using WinStudio.Permission.Common.Framework.Abstract;
using WinStudio.Permission.Models;

namespace WinStudio.Permission.Web.Controllers
{
    public class AccountController : AbstractPermissionMvcController
    {
        IAdministratorBusiService ibsAdministrator;
        public AccountController(IAdministratorBusiService bsAdministrator)
        {
            ibsAdministrator = bsAdministrator;
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string next)
        {
            ViewBag.NextUrl = next;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string next)
        {

            if (ModelState.IsValid)
            {
                ComRet ret = ibsAdministrator.Login(model.UserName, model.Password);
                if (!ret.Ret)
                    return View(model);
                FormsAuthentication.SetAuthCookie(ret.Instance<Administrator>().ToString(), false);
                //Reception.FinishLogin(HttpContext, model.UserName, PermissionConsts.WinPermissionCode);

                next = next.HasValue() ? next.FromBase64String() : PermissionConsts.WinPermissionProtalAddress;
                return Redirect(next);
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            ModelState.AddModelError("", "提供的用户名或密码不正确。");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Reception.LogOff(HttpContext);
            FormsAuthentication.SignOut();
            return RedirectToLocal(null);
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

    }
}
