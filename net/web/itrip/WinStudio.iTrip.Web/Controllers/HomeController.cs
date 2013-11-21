using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WinStudio.iTrip.Core.AbstractServices;

namespace WinStudio.iTrip.Web.Controllers
{
    public class HomeController : iTripController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private static DateTime LastNotificationTime = DateTime.Now;
        public void Notification()
        {
            var contexts = HttpContext.Application[MvcApplication.NotificationFlag] as List<HttpContextBase>;
            contexts.Add(HttpContext);

            int ss = (DateTime.Now - LastNotificationTime).Seconds;
            while (ss < 5)
            {
                int ts = (DateTime.Now - LastNotificationTime).Seconds;
                if (ts >= 3)
                {
                    LastNotificationTime = DateTime.Now;
                    if (HttpContext.Response.IsClientConnected)
                    {
                        HttpContext.Response.ContentType = "text/event-stream";
                        HttpContext.Response.CacheControl = "no-cache";
                        HttpContext.Response.Write(string.Concat("event: ", "meeting", "\n"));
                        HttpContext.Response.Write("data: {\"msg\": \"keep-alive" + DateTime.Now.ToString() + "\"}\n\n");
                        HttpContext.Response.Flush();
                    }
                }
            }
        }
    }
}
