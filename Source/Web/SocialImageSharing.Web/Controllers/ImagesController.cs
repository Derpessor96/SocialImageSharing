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
		private const string MimeType = "image/jpeg";

		public ImagesController(ISocialImageSharingData data)
			: base(data)
		{
		}

		[HttpGet]
		public ActionResult GetPostImage(int id)
		{
			var imageData = this.Data.Posts.GetById(id).Image;

			if (imageData == null)
			{
				return new HttpStatusCodeResult(404, "Image not found");
			}

			return File(imageData, MimeType);
		}
	}
}