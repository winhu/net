using System.Web.Mvc;

namespace WinStudio.iTrip.Web.Areas.Friend
{
    public class FriendAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Friend";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Friend_default",
                "Friend/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
