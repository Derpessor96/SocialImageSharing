using AutoMapper.QueryableExtensions;
using Kendo.Mvc.UI;
using SocialImageSharing.Data;
using SocialImageSharing.Data.Models;
using SocialImageSharing.Web.Areas.Administration.Controllers.Base;
using SocialImageSharing.Web.Areas.Administration.ViewModels.Comments;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialImageSharing.Web.Areas.Administration.Controllers
{
	public class CommentsController : KendoGridAdministrationController
	{
		public CommentsController(ISocialImageSharingData data)
			: base(data)
		{
		}

		public ActionResult Index()
		{
			return View();
		}

		protected override IEnumerable GetData()
		{
			return this.Data.PostComments.All()
				.Project()
				.To<CommentViewModel>();
		}

		protected override T GetById<T>(object id)
		{
			return this.Data.PostComments.GetById(id) as T;
		}

		[HttpPost]
		public ActionResult Update([DataSourceRequest]DataSourceRequest request, CommentViewModel model)
		{
			base.Update<PostComment, CommentViewModel>(model, model.Id);
			return this.GridOperation(model, request);
		}

		[HttpPost]
		public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, CommentViewModel model)
		{
			if (model != null && ModelState.IsValid)
			{
				this.Data.PostComments.Delete(model.Id);
				this.Data.SaveChanges();
			}

			return this.GridOperation(model, request);
		}
	}
}