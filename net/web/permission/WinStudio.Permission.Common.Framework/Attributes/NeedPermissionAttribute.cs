using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WinStudio.Framework.Authentication;

namespace System.Web.Mvc
{
    public class NeedPermissionAttribute : NeedAutoPermissionAttribute
    {
        public NeedPermissionAttribute(bool inheritPermission = true)
            : base(inheritPermission) { }
        public NeedPermissionAttribute(string name, bool inheritPermission = true)
            : base(name, inheritPermission) { }

        public override string HandlePermissionAddress
        {
            get { return WebConsts.WinHandlePermissionAddress; }
        }

        public override string HandleUnauthorizedAddress
        {
            get { return WebConsts.WinHandleLoginAddress; }
        }

        public override PermissionInfo GatherPermissionInfo(HttpContextBase context)
        {
            IWinSession session = context.GetFromSession<IWinSession>(context.GetToken());
            PermissionInfo info = new PermissionInfo() { UserAccount = session.Account, SysCode = "Permission" };
            return info;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!Reception.IsValidSession(httpContext))
            {
                if (Guider.IsAuthorized(httpContext))
                {
                    Reception.FinishLogin(httpContext, "Permission");
                    return true;
                }
            }
            return false;
            //return Guider.IsAuthorized(httpContext);
        }

    }
}
