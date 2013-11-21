using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WinStudio.Track.Web.Controllers
{
    public class HomeController : AbstractTrackController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [NeedPermission]
        public ActionResult MyPage()
        {
            return View();
        }

    }
}
