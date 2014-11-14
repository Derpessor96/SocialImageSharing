using AutoMapper;
using AutoMapper.QueryableExtensions;
using SocialImageSharing.Data;
using SocialImageSharing.Web.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialImageSharing.Web.Controllers
{
	public class PostsController : BaseController
	{
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

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public ActionResult MakePost()
		{

		}
	}
}