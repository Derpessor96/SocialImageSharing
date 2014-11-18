using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using SocialImageSharing.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using System.Data.Entity;
using SocialImageSharing.Data.Common.Models;
using SocialImageSharing.Web.Areas.Administration.ViewModels.Base;

namespace SocialImageSharing.Web.Areas.Administration.Controllers.Base
{
	public abstract class KendoGridAdministrationController : AdminController
	{
		public KendoGridAdministrationController(ISocialImageSharingData data)
			: base(data)
		{
		}

		protected abstract IEnumerable GetData();

		protected abstract T GetById<T>(object id) where T : class;

		[HttpPost]
		public ActionResult Read([DataSourceRequest]DataSourceRequest request)
		{
			var result =
				this.GetData()
				.ToDataSourceResult(request);

			return this.Json(result);
		}

		[NonAction]
		protected virtual T Create<T>(object model) where T : class
		{
			if (model != null && ModelState.IsValid)
			{
				var dbModel = Mapper.Map<T>(model);
				this.ChangeEntityStateAndSave(dbModel, EntityState.Added);
				return dbModel;
			}

			return null;
		}

		[NonAction]
		protected virtual void Update<TModel, TViewModel>(TViewModel model, object id)
			where TModel : AuditInfo
			where TViewModel : AdministrationViewModel
		{
			if (model != null && ModelState.IsValid)
			{
				var dbModel = this.GetById<TModel>(id);
				Mapper.Map<TViewModel, TModel>(model, dbModel);
				this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);
				model.ModifiedOn = dbModel.ModifiedOn;
			}
		}

		protected JsonResult GridOperation<T>(T model, [DataSourceRequest]DataSourceRequest request)
		{
			return Json(new[] { model }.ToDataSourceResult(request, ModelState));
		}

		private void ChangeEntityStateAndSave(object dbModel, EntityState state)
		{
			var entry = this.Data.Context.Entry(dbModel);
			entry.State = state;
			this.Data.SaveChanges();
		}
	}
}