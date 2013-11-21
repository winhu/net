using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinStudio.Passport.BusiService.Interfaces;
using WinStudio.Passport.Core.Permission;
using WinStudio.Passport.Models;

namespace WinStudio.Passport.Controllers
{
    [NeedSignIn("个人资料", false)]
    public class ProfileController : Controller
    {
        ISignBusiService bsSignServ;
        public ProfileController(ISignBusiService signServ)
        {
            bsSignServ = signServ;
        }
        //
        // GET: /Profile/

        //[NeedSignIn("概览")]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Profile/Details/5

        [NeedSignIn("详细")]
        public ActionResult Details(string account)
        {
            var ComRet = bsSignServ.GetProfile(account);
            return View(ComRet.Instance<Profile>());
        }

        //
        // POST: /Profile/Edit/5

        [NeedSignIn("编辑")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
