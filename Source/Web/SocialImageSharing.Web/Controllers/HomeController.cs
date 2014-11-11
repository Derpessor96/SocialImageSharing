using AutoMapper;
using SocialImageSharing.Data;
using SocialImageSharing.Data.Common.Repositories;
using SocialImageSharing.Data.Models;
using SocialImageSharing.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialImageSharing.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ISocialImageSharingData data;

		public HomeController(ISocialImageSharingData data)
		{
			this.data = data;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			var user = this.data.Users.All().FirstOrDefault();
			data.Users.Delete(user);
			data.SaveChanges();
			var userTest = Mapper.Map<UserTestViewModel>(user);
			return Content(userTest.Email);
		}

		public ActionResult Contact()
		{
			var userTest = Mapper.Map<UserTestViewModel>(this.data.Users.All().FirstOrDefault());
			return Content(userTest.Email);
		}
	}
}