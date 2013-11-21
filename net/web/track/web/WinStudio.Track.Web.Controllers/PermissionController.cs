using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WinStudio.Track.IBusiService;

namespace WinStudio.Track.Web.Controllers
{
    public class PermissionController : AbstractMvcController
    {
        public ActionResult Register()
        {
            var serv = GetService<ITimeLineBusiService>();
            if (serv == null) throw new Exception("null reference");
            return View();
        }
    }
}
