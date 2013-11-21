using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WinStudio.Framework.Authentication;

namespace System.Web.Mvc
{
    public class NeedPermissionAttribute : NeedAutoPermissionAttribute
    {
        public override string HandlePermissionAddress
        {
            get { return WebConsts.WinHandlePermissionAddress; }
        }

        public override string HandleUnauthorizedAddress
        {
            get { return WebConsts.WinHandleLoginAddress; }
        }

        public override PermissionInfo GatherPermissionInfo(System.Web.HttpContextBase httpContext)
        {
            IWinSession session = httpContext.GetFromSession<IWinSession>(httpContext.GetToken());
            PermissionInfo info = new PermissionInfo() { UserAccount = session.Account, SysCode = "Track" };
            return info;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!Reception.IsValidSession(httpContext))
            {
                if (Guider.IsAuthorized(httpContext))
                {
                    Reception.FinishLogin(httpContext, "Track");
                    return true;
                }
            }
            return false;
            //return Guider.IsAuthorized(httpContext);
        }
    }
}
