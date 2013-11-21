using System.Web.Mvc;

namespace WinStudio.Track.Web.Areas.Tracker
{
    public class TrackerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Tracker";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Tracker_default",
                "Tracker/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
