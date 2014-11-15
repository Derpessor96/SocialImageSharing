using AutoMapper;
using AutoMapper.QueryableExtensions;
using SocialImageSharing.Data;
using SocialImageSharing.Web.ViewModels.Posts;
using SocialImageSharing.Web.ViewModels.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialImageSharing.Web.Controllers
{
	[RoutePrefix("Profiles")]
	public class ProfilesController : BaseController
	{
		private const int pageSize = 12;

		public ProfilesController(ISocialImageSharingData data)
			: base(data)
		{
		}

		[HttpGet]
		[Authorize]
		public ActionResult GetUserPosts(int? page, string orderBy, string creatorId)
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
				.To<PostViewModel>()
				.Where(p => p.CreatorId == creatorId);

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

			return PartialView("~/Views/Shared/Posts.cshtml", posts.ToList());
		}

		[HttpGet]
		[Authorize]
		public ActionResult GetUserFavorites(int? page, string orderBy, string userId)
		{
			if (page == null)
			{
				page = 0;
			}

			if (orderBy == null)
			{
				orderBy = "rating";
			}

			var posts = this.Data.UserFavoritePosts.All()
				.Where(f => f.UserId == userId)
				.Select(f => f.Post)
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

			return PartialView("~/Views/Shared/Posts.cshtml", posts.ToList());
		}

		[HttpGet]
		[Authorize]
		[Route("{id}/PostsByRating")]
		public ActionResult PostsByRating(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(400, "User id is required");
			}

			var user = this.Data.Users.All()
				.FirstOrDefault(u => u.Id == id);

			if (user == null)
			{
				return new HttpStatusCodeResult(400, "No such user exists");
			}

			var userPostsViewModel = Mapper.Map<UserPostsViewModel>(user);

			return View(userPostsViewModel);
		}

		[HttpGet]
		[Authorize]
		[Route("{id}/PostsByDate")]
		public ActionResult PostsByDate(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(400, "User id is required");
			}

			var user = this.Data.Users.All()
				.FirstOrDefault(u => u.Id == id);

			if (user == null)
			{
				return new HttpStatusCodeResult(400, "No such user exists");
			}

			var userPostsViewModel = Mapper.Map<UserPostsViewModel>(user);

			return View(userPostsViewModel);
		}

		[HttpGet]
		[Authorize]
		[Route("{id}/FavoritesByRating")]
		public ActionResult FavoritesByRating(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(400, "User id is required");
			}

			var user = this.Data.Users.All()
				.FirstOrDefault(u => u.Id == id);

			if (user == null)
			{
				return new HttpStatusCodeResult(400, "No such user exists");
			}

			var userFavoritesViewModel = Mapper.Map<UserFavoritesViewModel>(user);

			return View(userFavoritesViewModel);
		}

		[HttpGet]
		[Authorize]
		[Route("{id}/FavoritesByDate")]
		public ActionResult FavoritesByDate(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(400, "User id is required");
			}

			var user = this.Data.Users.All()
				.FirstOrDefault(u => u.Id == id);

			if (user == null)
			{
				return new HttpStatusCodeResult(400, "No such user exists");
			}

			var userFavoritesViewModel = Mapper.Map<UserFavoritesViewModel>(user);

			return View(userFavoritesViewModel);
		}
	}
}