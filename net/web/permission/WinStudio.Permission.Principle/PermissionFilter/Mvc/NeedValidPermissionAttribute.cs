using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Permission.Principle.Interfaces;

namespace System.Web.Mvc
{
    public abstract class NeedValidPermissionAttribute : NeedSignInAttribute
    {
        private string roles = string.Empty;
        public NeedValidPermissionAttribute(string roles)
        {
            this.roles = roles;
        }
        public NeedValidPermissionAttribute()
        {
            this.roles = string.Empty;
        }

        public abstract string PermissionUpdateAddress { get; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!AuthorizeCore(filterContext.HttpContext))
                HandleUnauthorizedRequest(filterContext);
            else
            {
                INeedPermissionController ctrl = GetPermissionController(filterContext);
                IUserPermission up = filterContext.HttpContext.GetUserPermission();
                if (!up.HasPermission(ctrl.SysAuth, roles, ctrl.Key, ctrl.BusiCode))
                {
                    string permission = Utility.SendRequest(PermissionUpdateAddress, string.Format("Account={0}&SysAuth={1}", ctrl.Account, ctrl.SysAuth), filterContext.HttpContext.Request.Cookies, Method, Encoder);
                    up.UpdatePermission(permission);
                    if (!up.HasPermission(ctrl.SysAuth, roles, ctrl.Key, ctrl.BusiCode))
                        HandleUnauthorizedRequest(filterContext);
                }
            }
        }

        private INeedPermissionController GetPermissionController(AuthorizationContext context)
        {
            return context.Controller as INeedPermissionController;
        }
    }
}
