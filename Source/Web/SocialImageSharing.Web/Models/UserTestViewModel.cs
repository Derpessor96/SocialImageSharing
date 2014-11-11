using SocialImageSharing.Web.Infrastructure.Mapping;
using SocialImageSharing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialImageSharing.Web.Models
{
	public class UserTestViewModel : IMapFrom<User>
	{
		public string Id { get; set; }

		public string Email { get; set; }
	}
}