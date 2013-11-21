using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Permission.Principle;
using WinStudio.Permission.Principle.Mvc;

namespace System.Web.Mvc
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public abstract class NeedValidPermissionAttribute : AuthorizeAttribute
    {
        public NeedValidPermissionAttribute()
            : this(true)
        { }
        public NeedValidPermissionAttribute(bool inheritPermission = false)
        {
            InheritPermission = inheritPermission;
        }
        private bool InheritPermission = false;

        public virtual string JumpConnector { get { return "next"; } }
        public virtual bool NeedBase64EncryptJumpUrl { get { return true; } }
        public virtual Encoding Encoding { get { return Encoding.UTF8; } }
        public abstract string HandlePermissionAddress { get; }
        public abstract string HandleUnauthorizedAddress { get; }
        public abstract PermissionInfo GatherPermissionInfo(HttpContextBase httpContext);

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (InheritPermission || filterContext.IsActionNeedValidated())
            {
                if (!AuthorizeCore(filterContext.HttpContext))
                    HandleUnauthorizedRequest(filterContext);
                else
                {
                    PermissionInfo info = new GatherPermissionInfoAction(GatherPermissionInfo)(filterContext.HttpContext);
                    IUserPermission up = filterContext.HttpContext.GetUserPermission(info.UserAccount);
                    if (!up.HasPermission(filterContext.HttpContext, info, Roles))
                    {
                        string permission = PermissionMvcUtility.SendRequest(HandlePermissionAddress, null, filterContext.HttpContext.Request.Cookies, FormMethod.Post, Encoding, filterContext.HttpContext.BuildPermissionConfig(info));
                        up.UpdatePermission(permission);
                        if (!up.HasPermission(filterContext.HttpContext, info, Roles))
                            HandleUnauthorizedRequest(filterContext);
                    }
                }
            }
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
