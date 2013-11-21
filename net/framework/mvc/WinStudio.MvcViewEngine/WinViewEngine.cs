using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    /// <summary>
    /// Mvc视图引擎
    /// </summary>
    public class WinViewEngine : BuildManagerViewEngine
    {
        // Fields
        internal static readonly string ViewStartFileName = "_ViewStart";

        /// <summary>
        /// 自定义Mvc视图引擎
        /// </summary>
        /// <param name="areaPath">域路径</param>
        public WinViewEngine(string areaPath = "Areas")
            : this(null, areaPath)
        { }

        public WinViewEngine(IViewPageActivator viewPageActivator, string AreaPath)
            : base(viewPageActivator)
        {
            AreaViewLocationFormats = new[]
            {
                "~/"+AreaPath+"/{2}/Views/{1}/{0}.cshtml",
                "~/"+AreaPath+"/{2}/Views/{1}/{0}.vbhtml",
                "~/"+AreaPath+"/{2}/Views/Shared/{0}.cshtml",
                "~/"+AreaPath+"/{2}/Views/Shared/{0}.vbhtml"
            };
            AreaMasterLocationFormats = new[]
            {
                "~/"+AreaPath+"/{2}/Views/{1}/{0}.cshtml",
                "~/"+AreaPath+"/{2}/Views/{1}/{0}.vbhtml",
                "~/"+AreaPath+"/{2}/Views/Shared/{0}.cshtml",
                "~/"+AreaPath+"/{2}/Views/Shared/{0}.vbhtml"
            };
            AreaPartialViewLocationFormats = new[]
            {
                "~/"+AreaPath+"/{2}/Views/{1}/{0}.cshtml",
                "~/"+AreaPath+"/{2}/Views/{1}/{0}.vbhtml",
                "~/"+AreaPath+"/{2}/Views/Shared/{0}.cshtml",
                "~/"+AreaPath+"/{2}/Views/Shared/{0}.vbhtml"
            };

            ViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml"
            };
            MasterLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml"
            };
            PartialViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml"
            };

            FileExtensions = new[]
            {
                "cshtml",
                "vbhtml",
            };
        }
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            string layoutPath = null;
            bool runViewStartPages = false;
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, partialPath, layoutPath, runViewStartPages, fileExtensions, base.ViewPageActivator);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            string layoutPath = masterPath;
            bool runViewStartPages = true;
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, viewPath, layoutPath, runViewStartPages, fileExtensions, base.ViewPageActivator);
        }
        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return base.FileExists(controllerContext, virtualPath);
        }

    }
}
