using SocialImageSharing.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialImageSharing.Data.Models
{
	public class CommentLike : AuditInfo, IDeletableEntity
	{
		[Key]
		[Column(Order = 0)]
		public string UserId { get; set; }

		[InverseProperty("CommentLikes")]
		public virtual User User { get; set; }

		[Key]
		[Column(Order = 1)]
		public int CommentId { get; set; }

		[InverseProperty("Likes")]
		public virtual PostComment Comment { get; set; }

		[Range(-1, 1)]
		public byte Value { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }
	}
}
