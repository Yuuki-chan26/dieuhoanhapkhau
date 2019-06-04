using System.Web.Mvc;

namespace dieuhoanhapkhau.web.Areas.Admin
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
                 "NetAdvImage",
                 "Areas/admin/{scriptPath}/tinymce/plugins/netadvimage/{action}",
                 new { controller = "NetAdvImage" }
             );
            context.MapRoute(
                "admin",
                "admin/{controller}/{action}/{id}",
                new { Controller = "Account", action = "Admin", id = UrlParameter.Optional }
            );

        }
    }
}