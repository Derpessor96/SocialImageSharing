using SocialImageSharing.Data;
using SocialImageSharing.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialImageSharing.Web.Areas.Administration.Controllers
{
	public abstract class AdminController : BaseController
	{
		public AdminController(ISocialImageSharingData data)
			: base(data)
		{
		}
	}
}