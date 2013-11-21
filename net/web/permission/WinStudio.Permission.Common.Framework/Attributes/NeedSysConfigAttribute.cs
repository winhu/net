using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinStudio.Framework.Authentication;

namespace WinStudio.Permission.Common.Framework.Attributes
{
    public class NeedSysConfigAttribute : NeedHandleExceptionAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (null == filterContext.HttpContext.Application[WebConsts.WinWebApplicationConfigFlag])
                base.OnException(filterContext, "系统尚未配置，暂不能提供服务，给您带来的不便敬请谅解！如有需要，请联系系统管理员。");
            else base.OnActionExecuting(filterContext);
        }
    }
}