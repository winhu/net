using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutofacAutoResolver;
using WinStudio.ComResult;
using WinStudio.Framework.Web.Services;

namespace System.Web.Mvc
{
    public abstract class AbstractMvcController : Controller
    {
        private static IAutofacResolver _resolver = new AutofacResolver();
        protected T GetService<T>()
        {
            return _resolver.GetService<T>();
        }
        protected ActionResult RedirectToLocal(string url = null)
        {
            if (Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        protected ComRet Result() { return new ComRet(); }
        protected ComRet Result(bool ret, string msg) { return new ComRet(ret, msg); }
        protected ComRet Result(string err) { return new ComRet(err); }
        protected ComRet Result(object obj) { return new ComRet(obj); }
        protected ComRet Result(int num) { return new ComRet(num); }
        protected ComRet Result(bool ret, string msg, int num, object obj) { return new ComRet(ret, msg, num, obj); }

        protected JsonResult JsonMsg(bool ret = true, bool isTextHtml = false) { return JsonMsg(ret, null, 0, null, isTextHtml); }
        protected JsonResult JsonMsg(string msg, bool isTextHtml = false) { return JsonMsg(false, msg, 0, null, isTextHtml); }
        protected JsonResult JsonMsg(object obj, bool isTextHtml = false) { return JsonMsg(true, null, 0, obj, isTextHtml); }
        protected JsonResult JsonMsg(int id, bool isTextHtml = false) { return JsonMsg(id > 0, null, id, null, isTextHtml); }
        protected JsonResult JsonMsg(bool ret, object obj, bool isTextHtml = false) { return JsonMsg(ret, null, 0, obj, isTextHtml); }
        protected JsonResult JsonMsg(bool ret, int id, bool isTextHtml = false) { return JsonMsg(ret, null, id, null, isTextHtml); }
        protected JsonResult JsonMsg(bool ret, string msg, bool isTextHtml = false) { return JsonMsg(ret, msg, 0, null, isTextHtml); }
        protected JsonResult JsonMsg(ComRet ret, bool isTextHtml = false) { return JsonMsg(ret.Ret, ret.LastMsg, ret.Num, ret.Instance(0), isTextHtml); }
        protected JsonResult JsonMsg(bool ret, string msg, int id, object obj = null, bool isTextHtml = false)
        {
            if (!isTextHtml) return Json(new { ret = ret, msg = msg, id = id, obj = obj }, JsonRequestBehavior.AllowGet);
            else return Json(new { ret = ret, msg = msg, id = id, obj = obj }, "text/html", JsonRequestBehavior.AllowGet);
        }

        protected ViewResult ViewComRet<T>(ComRet ret)
        {
            if (ret.Ret) return View(ret.Instance<T>());
            return View(default(T));
        }
        protected ViewResult ViewComRet<T>(ComRet ret, int index)
        {
            if (ret.Ret) return View(ret.Instance(index));
            return View(default(T));
        }
        protected ViewResult ViewComRet<T>(T t)
        {
            return View(t);
        }


        /// <summary>
        /// 获取当前用户的IP地址
        /// </summary>
        /// <returns></returns>
        protected string GetIP()
        {
            //穿过代理服务器取远程用户真实IP地址
            string Ip = string.Empty;
            if (Request.ServerVariables["HTTP_VIA"] != null)
            {
                if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
                {
                    if (Request.ServerVariables["HTTP_CLIENT_IP"] != null)
                        Ip = Request.ServerVariables["HTTP_CLIENT_IP"].ToString();
                    else
                        if (Request.ServerVariables["REMOTE_ADDR"] != null)
                            Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        else
                            Ip = "127.0.0.1";
                }
                else
                    Ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (Request.ServerVariables["REMOTE_ADDR"] != null)
            {
                Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            else
            {
                Ip = "127.0.0.1";
            }
            return Ip;
        }
    }

    public abstract class AbstractAsyncTrackController : AsyncController
    {
        protected ActionResult RedirectToLocal(string url = null)
        {
            if (Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        protected ComRet Result() { return new ComRet(); }
        protected ComRet Result(bool ret, string msg) { return new ComRet(ret, msg); }
        protected ComRet Result(string err) { return new ComRet(err); }
        protected ComRet Result(object obj) { return new ComRet(obj); }
        protected ComRet Result(int num) { return new ComRet(num); }
        protected ComRet Result(bool ret, string msg, int num, object obj) { return new ComRet(ret, msg, num, obj); }
    }
}
