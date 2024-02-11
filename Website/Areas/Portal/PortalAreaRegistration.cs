using System.Web.Mvc;

namespace Website.Areas.Portal
{
    public class PortalAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Portal";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Portal_default",
                "Portal/{controller}/{action}/{id}",
                //new { action = "Index", id = UrlParameter.Optional }
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Website.Areas.Portal.Controllers" }
            );
        }
    }
}