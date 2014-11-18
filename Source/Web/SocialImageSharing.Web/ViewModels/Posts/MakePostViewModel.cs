using AutoMapper;
using SocialImageSharing.Data.Models;
using SocialImageSharing.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialImageSharing.Web.ViewModels.Posts
{
	public class MakePostViewModel : IMapFrom<Post>
	{
		[AllowHtml]
		[Required(ErrorMessage = "Post title is required!")]
		public string Title { get; set; }

		[AllowHtml]
		[Required(ErrorMessage = "Post description is required!")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Post image is required!")]
		public HttpPostedFileBase ImageFile { get; set; }

		public string ImageExtension { get; set; }
	}
}