using AutoMapper.QueryableExtensions;
using Kendo.Mvc.UI;
using SocialImageSharing.Data;
using SocialImageSharing.Data.Models;
using SocialImageSharing.Web.Areas.Administration.Controllers.Base;
using SocialImageSharing.Web.Areas.Administration.ViewModels.Posts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialImageSharing.Web.Areas.Administration.Controllers
{
	public class PostsController : KendoGridAdministrationController
	{
		public PostsController(ISocialImageSharingData data)
			: base(data)
		{
		}

		public ActionResult Index()
		{
			return View();
		}

		protected override IEnumerable GetData()
		{
			return this.Data.Posts.All()
				.Project()
				.To<PostViewModel>();
		}

		protected override T GetById<T>(object id)
		{
			return this.Data.Posts.GetById(id) as T;
		}

		[HttpPost]
		public ActionResult Update([DataSourceRequest]DataSourceRequest request, PostViewModel model)
		{
			base.Update<Post, PostViewModel>(model, model.Id);
			return this.GridOperation(model, request);
		}

		[HttpPost]
		public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, PostViewModel model)
		{
			if (model != null && ModelState.IsValid)
			{
				this.Data.Posts.Delete(model.Id);
				this.Data.SaveChanges();
			}

			return this.GridOperation(model, request);
		}
	}
}