using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinStudio.iTrip.Core.AbstractServices;

namespace WinStudio.iTrip.Web.Controllers
{
    [NeediTripPermission]
    public class MapController : iTripController
    {
        //
        // GET: /Map/

        public ActionResult Index()
        {
            if (!IsLogin)
                return RedirectToLocal("/Account/Login");
            return View();
        }

        public ActionResult MyLocation()
        {
            if (!IsLogin)
                return RedirectToLocal("/Account/Login");
            return View();
        }

    }
}
