using SocialImageSharing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialImageSharing.Web.ViewModels.Comments
{
	public class LikeCommentViewModel
	{
		public PostComment Comment { get; set; }

		public short UserLikeValue { get; set; }

		public int TotalLikeValue { get; set; }
	}
}