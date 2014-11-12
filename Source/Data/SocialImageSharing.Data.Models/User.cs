namespace SocialImageSharing.Data.Models
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using SocialImageSharing.Data.Common.Models;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Security.Claims;
	using System.Threading.Tasks;

	public class User : IdentityUser, IDeletableEntity, IAuditInfo
	{
		public User()
			: base()
		{
			this.Posts = new HashSet<Post>();
			this.CommentLikes = new HashSet<CommentLike>();
			this.PostLikes = new HashSet<PostLike>();
			this.PostComments = new HashSet<PostComment>();
		}

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}

		public virtual ICollection<Post> Posts { get; set; }

		public virtual ICollection<CommentLike> CommentLikes { get; set; }

		public virtual ICollection<PostLike> PostLikes { get; set; }

		public virtual ICollection<PostComment> PostComments { get; set; }

		public virtual ICollection<UserFavoritePost> Favorites { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }

		public DateTime CreatedOn { get; set; }

		[NotMapped]
		public bool PreserveCreatedOn { get; set; }

		public DateTime? ModifiedOn { get; set; }
	}
}
