using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinStudio.iTrip.Core.AbstractServices;
using WinStudio.iTrip.ICommonBusiness;

namespace WinStudio.iTrip.Web.Areas.ComServ.Controllers
{
    public class NationalityController : iTripController
    {
        //
        // GET: /ComServ/Nationality/

        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public ActionResult AllNationalityList()
        {
            var list = GetService<INationalityBusiness>().GetList();
            return View(list);
        }

        //[HttpPost]
        public ActionResult NativePlaceList(string nationCode)
        {
            var list = GetService<INationalityBusiness>().GetNativePlaceList(nationCode);
            return View(list);
        }
    }
}
