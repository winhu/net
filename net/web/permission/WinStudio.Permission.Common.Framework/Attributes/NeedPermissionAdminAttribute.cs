using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WinStudio.Permission.Common.Framework.Attributes
{
    public class NeedPermissionAdminAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            var urlnext = Convert.ToBase64String(Encoding.UTF8.GetBytes(filterContext.HttpContext.Request.Url.ToString()));
            var urllogin = string.Format("{0}?next={1}", PermissionConsts.WinPermissionAdminLoginAddress, urlnext);
            RedirectResult ret = new RedirectResult(urllogin);
            filterContext.Result = ret;
        }
    }
}
