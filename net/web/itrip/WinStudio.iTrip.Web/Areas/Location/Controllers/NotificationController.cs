using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinStudio.iTrip.Framework.Location;

namespace WinStudio.iTrip.Web.Areas.Location.Controllers
{
    [NeediTripPermission]
    public class NotificationController : Controller
    {
        //
        // GET: /Location/Notification/

        public ActionResult Index()
        {

            return View();
        }
        public void Subscribe()
        {
            var data = LocationService.Instance.GetAllUserLocations();

            HttpContext.Response.ContentType = "text/event-stream";
            HttpContext.Response.CacheControl = "no-cache";
            HttpContext.Response.Write("data:" + data + "\n\n");
            HttpContext.Response.Flush();
        }
        public void Publish()
        {
            PublishService.Publish();
        }

    }
}
