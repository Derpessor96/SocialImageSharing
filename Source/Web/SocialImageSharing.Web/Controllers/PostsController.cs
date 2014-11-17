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
		private static readonly string[] allowedExtensions = new string[] { "jpeg", "jpg" };
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

			return PartialView("Posts", posts.ToList());
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

			return RedirectToAction("PostDetails", new { id = newPost.Id });
		}

		[HttpGet]
		public ActionResult PostDetails(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(400, "Post requires an id");
			}

			var post = this.Data.Posts.All().FirstOrDefault(p => p.Id == id.Value);

			if (post == null)
			{
				return new HttpStatusCodeResult(404, "Post not found");
			}

			var viewModel = Mapper.Map<PostDetailsViewModel>(post);

			PostLike like = null;
			if (this.CurrentUser != null)
			{
				like = this.Data.PostLikes.All()
					.FirstOrDefault(l => l.PostId == post.Id && l.UserId == this.CurrentUser.Id);
			}

			if (like == null)
			{
				viewModel.LikeValue = 0;
			}
			else
			{
				viewModel.LikeValue = like.Value;
			}

			UserFavoritePost favorite = null;
			if (this.CurrentUser != null)
			{
				favorite = this.Data.UserFavoritePosts.All()
					.FirstOrDefault(f => f.PostId == post.Id && f.UserId == this.CurrentUser.Id);
			}

			viewModel.IsInFavorites = favorite != null;

			return View(viewModel);
		}

		[HttpPost]
		[Authorize]
		public ActionResult LikePost(int? id, short value)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(400, "Post requires an id");
			}

			var post = this.Data.Posts.All()
				.FirstOrDefault(p => p.Id == id.Value);

			if (post == null)
			{
				return new HttpStatusCodeResult(404, "Post not found");
			}

			if (value != -1 && value != 0 && value != 1)
			{
				return new HttpStatusCodeResult(400, "Value should be -1, 0 or 1");
			}

			var user = this.CurrentUser;
			var like = this.Data.PostLikes.All()
				.FirstOrDefault(l => l.PostId == post.Id && l.User.Id == user.Id);

			if (like == null)
			{
				like = new PostLike()
				{
					PostId = id.Value,
					Value = value,
					UserId = user.Id
				};

				this.Data.PostLikes.Add(like);
			}
			else
			{
				like.Value = value;
			}

			this.Data.SaveChanges();

			var postLikes = this.Data.PostLikes.All()
				.Where(l => l.PostId == post.Id);

			var resultViewModel = new LikePostViewModel()
			{
				Post = post,
				UserLikeValue = like.Value,
				TotalLikeValue =
					postLikes.Count(l => l.Value == 1) -
					postLikes.Count(l => l.Value == -1)
			};

			return PartialView(resultViewModel);
		}

		[HttpGet]
		[ChildActionOnly]
		public ActionResult LikePost(int id)
		{
			var post = this.Data.Posts.All()
				.FirstOrDefault(p => p.Id == id);

			var viewModel = new LikePostViewModel()
			{
				Post = post
			};

			if (Request.IsAuthenticated)
			{
				var userId = this.CurrentUser.Id;

				var currentUserLike = this.Data.PostLikes.All()
					.FirstOrDefault(l => l.PostId == id && l.UserId == userId);

				viewModel.UserLikeValue = currentUserLike == null ? (short)0 : currentUserLike.Value;
			}

			var postLikes = this.Data.PostLikes.All()
				.Where(l => l.PostId == post.Id);

			viewModel.TotalLikeValue =
				postLikes.Count(l => l.Value == 1) -
				postLikes.Count(l => l.Value == -1);

			return PartialView(viewModel);
		}

		[HttpPost]
		[Authorize]
		public ActionResult FavoritePost(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(400, "Post requires an id");
			}

			var post = this.Data.Posts.All()
				.FirstOrDefault(p => p.Id == id);

			if (post == null)
			{
				return new HttpStatusCodeResult(404, "Post not found");
			}

			var favorite = this.Data.UserFavoritePosts.All()
				.FirstOrDefault(f => f.PostId == id.Value && f.UserId == this.CurrentUser.Id);

			if (favorite != null)
			{
				return new HttpStatusCodeResult(400, "Post already in favorites");
			}
			else
			{
				this.Data.UserFavoritePosts.Add(new UserFavoritePost()
				{
					UserId = this.CurrentUser.Id,
					PostId = id.Value
				});
				this.Data.SaveChanges();

				return Content("");
			}
		}

		[HttpPost]
		[Authorize]
		public ActionResult UnfavoritePost(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(400, "Post requires an id");
			}

			var post = this.Data.Posts.All()
				.FirstOrDefault(p => p.Id == id);

			if (post == null)
			{
				return new HttpStatusCodeResult(404, "Post not found");
			}

			var favorite = this.Data.UserFavoritePosts.All()
				.FirstOrDefault(f => f.PostId == id.Value && f.UserId == this.CurrentUser.Id);

			if (favorite == null)
			{
				return new HttpStatusCodeResult(400, "Post is not in favorites");
			}
			else
			{
				this.Data.UserFavoritePosts.HardDelete(favorite);
				this.Data.SaveChanges();

				return Content("");
			}
		}
	}
}