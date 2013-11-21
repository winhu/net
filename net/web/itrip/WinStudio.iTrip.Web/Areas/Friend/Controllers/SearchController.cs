using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinStudio.iTrip.Core.AbstractServices;
using WinStudio.iTrip.IBusiness;

namespace WinStudio.iTrip.Web.Areas.Friend.Controllers
{
    [NeediTripPermission]
    public class SearchController : iTripController
    {
        //
        // GET: /Friend/Search/

        public ActionResult Index(string name = "")
        {
            ViewBag.Friends = GetService<IProfileBusiness>().GetFriends(name);
            return View();
        }

    }
}
