namespace SocialImageSharing.Data.Models
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using SocialImageSharing.Data.Common.Models;
	using System;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Security.Claims;
	using System.Threading.Tasks;

	public class User : IdentityUser, IDeletableEntity, IAuditInfo
	{
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}

		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }

		public DateTime CreatedOn { get; set; }
		
		[NotMapped]
		public bool PreserveCreatedOn { get; set; }

		public DateTime? ModifiedOn { get; set; }
	}
}
