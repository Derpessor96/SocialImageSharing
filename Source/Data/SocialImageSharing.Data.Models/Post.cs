using SocialImageSharing.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialImageSharing.Data.Models
{
	public class Post : AuditInfo, IDeletableEntity
	{
		public Post()
		{
			this.Likes = new HashSet<PostLike>();
			this.Comments = new HashSet<PostComment>();
			this.Favorites = new HashSet<UserFavoritePost>();
		}

		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public byte[] Image { get; set; }

		public string ImageExtension { get; set; }

		public string CreatorId { get; set; }

		[InverseProperty("Posts")]
		public virtual User Creator { get; set; }

		public virtual ICollection<PostLike> Likes { get; set; }

		public virtual ICollection<PostComment> Comments { get; set; }

		public virtual ICollection<UserFavoritePost> Favorites { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }
	}
}
