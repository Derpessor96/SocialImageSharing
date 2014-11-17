using AutoMapper;
using AutoMapper.QueryableExtensions;
using SocialImageSharing.Data;
using SocialImageSharing.Data.Models;
using SocialImageSharing.Web.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialImageSharing.Web.Controllers
{
	public class CommentsController : BaseController
	{
		public CommentsController(ISocialImageSharingData data)
			: base(data)
		{
		}

		[HttpGet]
		public ActionResult PostComments(int? postId)
		{
			if (postId == null)
			{
				return new HttpStatusCodeResult(400, "Post requires an id");
			}

			var post = this.Data.Posts.All()
				.FirstOrDefault(p => p.Id == postId.Value);

			if (post == null)
			{
				return new HttpStatusCodeResult(404, "Post not found");
			}

			var commentViewModels = this.Data.PostComments.All()
				.Where(c => c.PostId == postId)
				.OrderByDescending(c => c.CreatedOn)
				.Project()
				.To<CommentViewModel>();

			foreach (var comment in commentViewModels)
			{
				var commentLike = this.Data.CommentLikes.All()
					.FirstOrDefault(cl => cl.UserId == comment.CreatorId && cl.CommentId == comment.Id);

				if (commentLike == null)
				{
					comment.LikeValue = 0;
				}
				else
				{
					comment.LikeValue = commentLike.Value;
				}
			}

			ViewBag.PostId = postId;
			return View(commentViewModels);
		}

		[HttpPost]
		[Authorize]
		public ActionResult MakeComment(int? postId, CommentViewModel newComment)
		{
			if (postId == null)
			{
				return new HttpStatusCodeResult(400, "Post requires an id");
			}

			var post = this.Data.Posts.All()
				.FirstOrDefault(p => p.Id == postId.Value);

			if (post == null)
			{
				return new HttpStatusCodeResult(404, "Post not found");
			}

			if (!ModelState.IsValid)
			{
				return new HttpStatusCodeResult(400, "Failed to add comment");
			}

			var newDbComment = Mapper.Map<PostComment>(newComment);

			newDbComment.UserId = this.CurrentUser.Id;

			this.Data.PostComments.Add(newDbComment);
			this.Data.SaveChanges();

			return PartialView("SingleComment", Mapper.Map<CommentViewModel>(newDbComment));
		}

		[HttpGet]
		public ActionResult LikeComment(int id)
		{
			var comment = this.Data.PostComments.All()
				.FirstOrDefault(c => c.Id == id);

			var viewModel = new LikeCommentViewModel()
			{
				Comment = comment
			};

			if (Request.IsAuthenticated)
			{
				var userId = this.CurrentUser.Id;

				var currentUserLike = this.Data.CommentLikes.All()
					.FirstOrDefault(l => l.CommentId == id && l.UserId == userId);

				viewModel.UserLikeValue = currentUserLike == null ? (short)0 : currentUserLike.Value;
			}

			var commentLikes = this.Data.CommentLikes.All()
				.Where(l => l.CommentId == comment.Id);

			viewModel.TotalLikeValue =
				commentLikes.Count(l => l.Value == 1) -
				commentLikes.Count(l => l.Value == -1);

			return PartialView(viewModel);
		}

		[HttpPost]
		[Authorize]
		public ActionResult LikeComment(int? id, short? value)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(400, "Comment requires an id");
			}

			if (value == null)
			{
				return this.LikeComment(id.Value);
			}

			var comment = this.Data.PostComments.All()
				.FirstOrDefault(c => c.Id == id.Value);

			if (comment == null)
			{
				return new HttpStatusCodeResult(404, "Comment not found");
			}

			if (value != -1 && value != 0 && value != 1)
			{
				return new HttpStatusCodeResult(400, "Value should be -1, 0 or 1");
			}

			var user = this.CurrentUser;
			var like = this.Data.CommentLikes.All()
				.FirstOrDefault(l => l.CommentId == comment.Id && l.User.Id == user.Id);

			if (like == null)
			{
				like = new CommentLike()
				{
					CommentId = id.Value,
					Value = value.Value,
					UserId = user.Id
				};

				this.Data.CommentLikes.Add(like);
			}
			else
			{
				like.Value = value.Value;
			}

			this.Data.SaveChanges();

			var commentLikes = this.Data.CommentLikes.All()
				.Where(l => l.CommentId == comment.Id);

			var resultViewModel = new LikeCommentViewModel()
			{
				Comment = comment,
				UserLikeValue = like.Value,
				TotalLikeValue =
					commentLikes.Count(l => l.Value == 1) -
					commentLikes.Count(l => l.Value == -1)
			};

			return PartialView(resultViewModel);
		}
	}
}