using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WinStudio.Framework.Authentication;

namespace WinStudio.Permission.Common.Framework.Attributes
{
    public class NeedAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return Reception.IsExpiredSession(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            RedirectResult ret = new RedirectResult(WebConsts.WinHandleLoginAddress);
            filterContext.Result = ret;
        }
    }
}
