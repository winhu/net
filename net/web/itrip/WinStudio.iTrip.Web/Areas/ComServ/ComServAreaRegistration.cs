using System.Web.Mvc;

namespace WinStudio.iTrip.Web.Areas.ComServ
{
    public class ComServAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ComServ";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ComServ_default",
                "ComServ/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
