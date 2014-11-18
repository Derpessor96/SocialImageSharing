using System.Web.Mvc;

namespace SocialImageSharing.Web.Areas.Administration
{
	public class AdministrationAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Administration";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Administration_default",
				"Administration/{controller}/{action}/{id}",
				defaults: new { action = "Index", id = UrlParameter.Optional },
				namespaces: new string[] { "SocialImageSharing.Web.Areas.Administration.Controllers" }
			);
		}
	}
}