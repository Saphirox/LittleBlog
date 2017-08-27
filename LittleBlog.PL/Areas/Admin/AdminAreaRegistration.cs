using System.Web.Mvc;

namespace LittleBlog.PL.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }//,
                //new[] { "LittleBlog.PL.Areas.Admin.Controllers" }
            );
        }
    }
}