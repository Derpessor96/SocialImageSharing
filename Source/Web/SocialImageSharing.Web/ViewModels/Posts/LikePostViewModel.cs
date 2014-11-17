using SocialImageSharing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialImageSharing.Web.ViewModels.Posts
{
	public class LikePostViewModel
	{
		public Post Post { get; set; }

		public short UserLikeValue { get; set; }

		public int TotalLikeValue { get; set; }
	}
}