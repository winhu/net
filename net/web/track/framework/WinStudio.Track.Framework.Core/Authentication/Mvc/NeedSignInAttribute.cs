using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public class NeedSignInAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated) return true;
            return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            RedirectResult ret = new RedirectResult("http://localhost:9050/Account/Login?returnUrl=" + filterContext.RequestContext.HttpContext.Request.Url.OriginalString.ToBase64String());
            filterContext.Result = ret;
        }
    }
}
