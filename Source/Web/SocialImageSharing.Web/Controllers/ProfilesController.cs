using SocialImageSharing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialImageSharing.Web.Controllers
{
	public class ProfilesController : BaseController
	{
		public ProfilesController(ISocialImageSharingData data)
			: base(data)
		{
		}
	}
}