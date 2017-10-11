using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Twilio.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration 
    {
        public override string AreaName
        {
            get { return "Api"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapRoute(
                "AccountController",
                "api/v1/account/{id}/{action}",
                new { controller = "Account" }
            );
        }
    }
}