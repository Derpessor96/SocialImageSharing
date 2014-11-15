using AutoMapper;
using AutoMapper.QueryableExtensions;
using SocialImageSharing.Common.Extensions;
using SocialImageSharing.Data;
using SocialImageSharing.Data.Models;
using SocialImageSharing.Web.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialImageSharing.Web.Controllers
{
	public class PostsController : BaseController
	{
		private static string[] allowedExtensions = new string[] { "jpeg", "jpg" };
		private const int pageSize = 12;

		public PostsController(ISocialImageSharingData data)
			: base(data)
		{
		}

		[HttpGet]
		public ActionResult Index()
		{
			return RedirectToAction("Best");
		}

		[HttpGet]
		public ActionResult Best()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Latest()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Posts(int? page, string orderBy)
		{
			if (page == null)
			{
				page = 0;
			}

			if (orderBy == null)
			{
				orderBy = "rating";
			}

			var posts = this.Data.Posts.All()
				.Project()
				.To<PostViewModel>();

			if (orderBy == "rating")
			{
				posts = posts.OrderByDescending(p => p.Rating);
			}
			else if (orderBy == "date" || orderBy == null)
			{
				posts = posts.OrderByDescending(p => p.CreatedOn);
			}

			posts = posts
				.Skip(page.Value * pageSize)
				.Take(pageSize);

			return PartialView(posts.ToList());
		}

		[HttpGet]
		[Authorize]
		public ActionResult MakePost()
		{
			var model = new MakePostViewModel();
			return View(model);
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public ActionResult MakePost(MakePostViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var fileExtension = model.ImageFile.GetExtension();

			if (allowedExtensions.Any(e => e == fileExtension))
			{
				model.ImageExtension = fileExtension;
			}
			else
			{
				ModelState.AddModelError("", "File format not allowed!");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var newPost = Mapper.Map<Post>(model);
			newPost.Creator = this.CurrentUser;

			using (var binaryStream = new BinaryReader(model.ImageFile.InputStream))
			{
				newPost.Image = binaryStream.ReadBytes(model.ImageFile.ContentLength);
			}

			this.Data.Posts.Add(newPost);
			this.Data.SaveChanges();

			// TODO: Redirect to post details
			return RedirectToAction("Latest");
		}
	}
}