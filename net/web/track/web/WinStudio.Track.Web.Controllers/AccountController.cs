using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WinStudio.Track.IBusiService;
using WinStudio.Track.Models.Tracking;

namespace WinStudio.Track.Web.Controllers
{
    public class AccountController : AbstractMvcController
    {
        public ActionResult Register() { return View(); }

        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            var ibsCusomter = GetService<ICustomerBusiService>();
            var ret = ibsCusomter.Register(customer);
            ModelState.AddModelError("", ret.Ret ? "注册成功！" : "注册失败！");
            return View();
        }
    }
}
