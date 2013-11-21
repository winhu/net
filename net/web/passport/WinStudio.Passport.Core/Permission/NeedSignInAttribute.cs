using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WinStudio.Framework.Authentication;

namespace WinStudio.Passport.Core.Permission
{
    public class NeedSignInAttribute : NeedAutoPermissionAttribute
    {
        public NeedSignInAttribute() { }
        public NeedSignInAttribute(string name, bool inheritPermission = true) : base(name, inheritPermission) { }
        public override string HandlePermissionAddress
        {
            get { return WebConsts.WinHandlePermissionAddress; }
        }

        public override string HandleUnauthorizedAddress
        {
            get { return WebConsts.WinHandleLoginAddress; }
        }

        public override PermissionInfo GatherPermissionInfo(HttpContextBase httpContext)
        {
            IWinSession session = httpContext.GetFromSession<IWinSession>(httpContext.GetToken());
            PermissionInfo info = new PermissionInfo() { UserAccount = session.Account, SysCode = "Passport" };
            return info;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return Guider.IsAuthorized(httpContext);
        }
    }
}
