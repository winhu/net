using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinStudio.iTrip.Core.AbstractServices;
using WinStudio.iTrip.IBusiness;

namespace WinStudio.iTrip.Web.Areas.Client.Controllers
{
    [NeediTripPermission]
    public class PrimaryController : iTripController
    {
        //
        // GET: /Client/Primary/

        public ActionResult Index()
        {
            var ibsProfile = GetService<IProfileBusiness>();
            var profile = ibsProfile.GetProfile(MyAccount);
            return View(profile);
        }

    }
}
