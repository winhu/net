using System.Web.Mvc;

namespace WinStudio.Track.Web.Areas.Primary
{
    public class PrimaryAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Primary";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Primary_default",
                "Primary/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
