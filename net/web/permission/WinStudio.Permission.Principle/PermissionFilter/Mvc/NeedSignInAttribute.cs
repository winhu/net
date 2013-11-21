using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public abstract class NeedSignInAttribute : AuthorizeAttribute
    {
        public abstract string TokenName { get; }
        public abstract FormMethod Method { get; }
        public abstract Encoding Encoder { get; }
        public abstract string VerifyResult { get; }
        public abstract string VerifyAddress { get; }
        public abstract string SignInAddress { get; }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            string token = httpContext.GeToken(TokenName);
            string ret = Utility.SendRequest(VerifyAddress, string.Format("{0}={1}", TokenName, token), httpContext.Request.Cookies, Method, Encoder);
            return VerifyResult == ret;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 403)
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            RedirectResult ret = new RedirectResult(SignInAddress);
            filterContext.Result = ret;
        }
    }
}
