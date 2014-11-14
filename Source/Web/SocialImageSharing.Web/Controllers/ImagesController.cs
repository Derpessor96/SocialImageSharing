using SocialImageSharing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialImageSharing.Web.Controllers
{
	public class ImagesController : BaseController
	{
		public ImagesController(ISocialImageSharingData data)
			: base(data)
		{
		}

		[HttpGet]
		public ActionResult GetPostImage(int id)
		{
			var imageData = this.Data.Posts.GetById(id).Image;
			// TODO: Check the content type
			return File(imageData, "image/jpeg");
		}
	}
}