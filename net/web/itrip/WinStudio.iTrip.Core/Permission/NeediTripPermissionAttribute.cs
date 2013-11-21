using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Permission;

namespace System.Web.Mvc
{
    public class NeediTripPermissionAttribute : NeedPermissionAttribute
    {
        public virtual string JumpConnector { get { return "next"; } }
        public virtual bool NeedBase64EncryptJumpUrl { get { return true; } }
        public virtual Encoding Encoding { get { return Encoding.UTF8; } }
        //public string HandlePermissionAddress { get { return WinWebGlobalManager.Config.WinHandleUnAuthorizedAddress} }
        public string HandleUnauthorizedAddress { get { return WinWebGlobalManager.Config.WinHandleUnAuthorizedAddress; } }
        public override bool ValidatePermission(AuthorizationContext filterContext)
        {
            return true;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            var urlnext = NeedBase64EncryptJumpUrl ? Convert.ToBase64String(Encoding.GetBytes(filterContext.HttpContext.Request.Url.ToString())) : filterContext.HttpContext.Request.Url.ToString();
            var urllogin = string.Format("{0}?{1}={2}", HandleUnauthorizedAddress, JumpConnector, urlnext);
            RedirectResult ret = new RedirectResult(urllogin);
            filterContext.Result = ret;
        }
    }
}
