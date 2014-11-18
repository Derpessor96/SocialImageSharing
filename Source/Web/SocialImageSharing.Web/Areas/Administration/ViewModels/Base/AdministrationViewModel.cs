using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialImageSharing.Web.Areas.Administration.ViewModels.Base
{
	public abstract class AdministrationViewModel
	{
		[HiddenInput(DisplayValue = false)]
		public DateTime CreatedOn { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime? ModifiedOn { get; set; }
	}
}