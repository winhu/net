using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinStudio.iTrip.Core.AbstractServices;
using WinStudio.iTrip.ICommonBusiness;

namespace WinStudio.iTrip.Web.Areas.ComServ.Controllers
{
    public class NativePlaceController : iTripController
    {
        //
        // GET: /ComServ/NativePlace/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(string nationCode)
        {
            var np = GetService<INationalityBusiness>().GetNativePlaceList(nationCode);
            return View(np);
        }

    }
}
