using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinStudio.iTrip.Core.AbstractServices;
using WinStudio.iTrip.Framework.Location;

namespace WinStudio.iTrip.Web.Areas.Location.Controllers
{
    [NeediTripPermission]
    public class MineController : iTripController
    {
        //
        // GET: /Location/Mine/

        public ActionResult Index()
        {
            return View();
        }

        public void Update(float longitude, float latitude)
        {
            LocationService.Instance.Update(MyAccount, longitude, latitude, MyName);
        }

    }
}
