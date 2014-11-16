using SocialImageSharing.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SocialImageSharing.Data.Models
{
	public class PostComment : AuditInfo, IDeletableEntity
	{
		public PostComment()
		{
			this.Likes = new HashSet<CommentLike>();
		}

		public int Id { get; set; }

		public string Content { get; set; }

		public string UserId { get; set; }

		[InverseProperty("PostComments")]
		public virtual User User { get; set; }

		public int PostId { get; set; }

		[InverseProperty("Comments")]
		public virtual Post Post { get; set; }

		public ICollection<CommentLike> Likes { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }
	}
}
