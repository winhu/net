using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace System.Web.Mvc
{
    /// <summary>
    /// MVC异常处理：出现异常跳转到指定页面，并将错误信息根据指定参数赋值。
    /// </summary>
    public abstract class HandleMvcExceptionAttribute : ActionFilterAttribute, IExceptionFilter
    {
        static HandleMvcExceptionAttribute()
        {
            debugFlag = IsDebug();
        }
        private static bool DebugMode { get { return debugFlag; } }
        private static bool debugFlag = false;
        public abstract void Log(Exception e);
        /// <summary>
        /// MVC异常处理：出现异常跳转到指定页面，并将错误信息根据指定参数赋值。若没有指定错误路径，则默认为Error页面。
        /// </summary>
        /// <param name="code">错误参数名</param>
        /// <param name="action">指定的Action</param>
        /// <param name="controller">指定的Controller</param>
        /// <param name="area">指定的Area</param>
        public HandleMvcExceptionAttribute(string code, string action, string controller, string area = null)
        {
            Action = action;
            Controller = controller;
            Area = area;
            Code = code;
        }
        private string Action;
        private string Controller;
        private string Area;
        private string Code;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        protected void OnException(ActionExecutingContext filterContext, string msg)
        {
            OnException(new ExceptionContext(filterContext.Controller.ControllerContext, new Exception(msg)));
            filterContext.Result = exceptionActionResult;
        }

        public ActionResult ExceptionActionResult { get { return exceptionActionResult; } }
        private ActionResult exceptionActionResult;
        public void OnException(ExceptionContext filterContext)
        {
            try
            {
                var account = filterContext.HttpContext.User.Identity.Name;
                var ip = filterContext.HttpContext.Request.UserHostAddress;
                var url = filterContext.HttpContext.Request.Url.ToString();
                var method = filterContext.HttpContext.Request.HttpMethod;

                //query params
                var queryparams = new StringBuilder();
                foreach (var k in filterContext.HttpContext.Request.QueryString.AllKeys)
                {
                    queryparams.Append(k).Append("=").Append(filterContext.HttpContext.Request.QueryString[k]).Append(",");
                }
                var queryParams = queryparams.ToString();

                //post parsms
                var postparsms = new StringBuilder();
                foreach (var k in filterContext.HttpContext.Request.Form.AllKeys)
                {
                    postparsms.Append(k).Append("=").Append(filterContext.HttpContext.Request.Form[k]).Append(",");
                }
                var postParams = postparsms.ToString();

                //ajax params
                var inputStr = new byte[filterContext.HttpContext.Request.InputStream.Length];
                filterContext.HttpContext.Request.InputStream.Read(inputStr, 0, inputStr.Length);
                var ajaxParams = Encoding.Default.GetString(inputStr);

                //exception log
                var error = new StringBuilder();
                error.Append(filterContext.Exception.Message).Append("\r\n<br/>");
                if (DebugMode)
                {
                    error.Append("asp.net mvc exception: ").Append("\r\n<br/>");
                    error.Append("\r\n").Append("account= ").Append(account).Append("\r\n<br/>");
                    error.Append("ip: ").Append(ip).Append("\r\n<br/>");
                    error.Append("url: ").Append(url).Append("\r\n<br/>");
                    error.Append("method: ").Append(method).Append("\r\n<br/>");
                    error.Append("query params: ").Append(queryParams).Append("\r\n<br/>");
                    error.Append("post params: ").Append(postParams).Append("\r\n<br/>");
                    error.Append("ajax params: ").Append(ajaxParams).Append("\r\n<br/>");
                    error.Append("exception stacktrace: ").Append(filterContext.Exception.StackTrace).Append("\r\n<br/>");
                    if (filterContext.Exception.InnerException != null)
                        error.Append(filterContext.Exception.InnerException.Message).Append("\r\n<br/>");
                }
                Log(new Exception(error.ToString()));

                if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    filterContext.HttpContext.Response.ClearContent();
                    filterContext.HttpContext.Response.Write(error.ToString().Replace("\r\n", "<br/>"));
                    filterContext.HttpContext.Response.StatusCode = 500;
                }

                if (string.IsNullOrEmpty(Controller) && string.IsNullOrEmpty(Action))
                {
                    ViewResult result = new ViewResult();
                    result.ViewData[Code] = error.ToString();
                    result.ViewName = "Error";
                    filterContext.Result = result;
                }
                else
                {
                    //写入异常
                    var tempData = new TempDataDictionary { };
                    tempData.Add(Code, error.ToString());
                    System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();
                    rvd.Add("Controller", Controller);
                    rvd.Add("Action", Action);
                    if (!string.IsNullOrEmpty(Area))
                        rvd.Add("Area", Area);
                    filterContext.Result = new WinMvcExceptionResult(rvd, tempData);
                }
                exceptionActionResult = filterContext.Result;
                filterContext.ExceptionHandled = true;
            }
            catch (Exception e)
            {
                Log(e);
            }
        }

        /// <summary>
        /// web程序是否为调试状态 
        /// 判断webconfig中 compilation debug="true"
        /// </summary>
        /// <returns></returns>
        private static bool IsDebug()
        {
            try
            {
                System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration("/Web.config");
                CompilationSection configSection = (CompilationSection)config.GetSection("system.web/compilation");
                return configSection.Debug;
            }
            catch
            {
                return false;
            }
        }
    }

    public class WinMvcExceptionResult : RedirectToRouteResult
    {
        public WinMvcExceptionResult(RouteValueDictionary route, TempDataDictionary viewdata)
            : base(route)
        {
            this.viewdata = viewdata;
        }
        public WinMvcExceptionResult(string routename, RouteValueDictionary route, bool permanent, TempDataDictionary viewdata)
            : base(routename, route, permanent)
        {
            this.viewdata = viewdata;
        }
        private TempDataDictionary viewdata = null;
        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);
            if (viewdata != null)
            {
                foreach (string key in viewdata.Keys)
                {
                    context.Controller.TempData.Remove(key);
                    context.Controller.TempData.Add(key, viewdata[key]);
                }
            }
        }
    }
}
