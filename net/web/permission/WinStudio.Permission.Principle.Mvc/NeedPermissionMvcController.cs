using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    /// <summary>
    /// 使用权限系统，请继承该抽象类（该类已经继承了System.Web.Mvc.Controller和System.Web.Mvc.INeedPermissionController）
    /// </summary>
    public abstract class NeedPermissionMvcController : Controller, INeedPermissionController
    {
        public abstract string Account { get; }

        public abstract string BusiCode { get; }

        public abstract string SysCode { get; }

        public string SysAuth
        {
            get { return string.Format("{0}://{1}", HttpContext.Request.Url.Scheme, HttpContext.Request.Url.Authority); }
        }

        public string Key
        {
            get { return PermissionUtility.ByPermissionEncrypter(HttpContext.Request.Url.AbsolutePath); }
        }
    }
}
