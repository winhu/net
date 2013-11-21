using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WinStudio.Permission.Common.Framework.Attributes;

namespace WinStudio.Permission.Common.Framework.Mvc
{
    [NeedSysConfig]
    [NeedAuthorization]
    public class BaseMvcController : Controller
    {
        #region JsonMsg
        public JsonResult JsonMsg(bool ret = true, bool istexthtml = false)
        {
            return JsonMsg(ret, null, 0, null, istexthtml);
        }
        protected JsonResult JsonMsg(bool ret, string msg, object obj, bool istexthtml = false)
        {
            return JsonMsg(ret, msg, 0, obj, istexthtml);
        }
        protected JsonResult JsonMsg(bool ret, string msg, int id, bool istexthtml = false)
        {
            return JsonMsg(ret, msg, id, null, istexthtml);
        }
        protected JsonResult JsonMsg(bool ret, string msg, bool istexthtml = false)
        {
            return JsonMsg(ret, msg, 0, null, istexthtml);
        }
        protected JsonResult JsonMsg(bool ret, string msg, int id, object obj, bool istexthtml)
        {
            if (!istexthtml)
                return Json(new { ret = ret, msg = msg, id = id, obj = obj }, "text/html", JsonRequestBehavior.AllowGet);
            return Json(new { ret = ret, msg = msg, id = id, obj = obj }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 帮助程序
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        protected ActionResult RedirectToError(string err)
        {
            return RedirectToAction("Index", "Exception");
        }

        protected void SaveTempData<T>(string key, T data)
        {
            HttpContext.Session[key] = data;
        }
        protected T GetTempData<T>(string key, bool remove = true)
        {
            var data = HttpContext.Session[key];
            if (remove)
                HttpContext.Session.Remove(key);
            if (data == null) return default(T);
            return (T)data;
        }
        #endregion
    }
}
