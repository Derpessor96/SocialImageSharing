using SocialImageSharing.Data;
using SocialImageSharing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Microsoft.AspNet.Identity;

namespace SocialImageSharing.Web.Controllers
{
    public class BaseController : Controller
    {
		public BaseController(ISocialImageSharingData data)
		{
			this.Data = data;
		}

		protected ISocialImageSharingData Data { get; set; }

		protected User CurrentUser { get; set; }

		protected override void Initialize(RequestContext requestContext)
		{
			base.Initialize(requestContext);

			var currentUserId = requestContext.HttpContext.User.Identity.GetUserId();
			this.CurrentUser = this.Data.Users.GetById(currentUserId);
		}
    }
}