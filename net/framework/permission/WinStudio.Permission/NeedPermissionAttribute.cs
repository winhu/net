using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WinStudio.Permission;

namespace System.Web.Mvc
{
    public abstract class NeedPermissionAttribute : AuthorizeAttribute
    {
        public NeedPermissionAttribute(bool isClient = false) { _isClient = isClient; }

        private bool _isClient = false;
        public bool IsClient { get { return _isClient; } }

        public abstract bool ValidatePermission(AuthorizationContext filterContext);

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (IsClient)
                return WinWebGlobalManager.Guider.IsAuthorized(httpContext);
            else return WinWebGlobalManager.Reception.IsAuthorized(httpContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!AuthorizeCore(filterContext.HttpContext))
                HandleUnauthorizedRequest(filterContext);
            else if (!ValidatePermission(filterContext))
                HandleUnauthorizedRequest(filterContext);
        }
    }
}
