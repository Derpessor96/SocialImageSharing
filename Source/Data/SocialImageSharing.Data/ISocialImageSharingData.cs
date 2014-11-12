using SocialImageSharing.Data.Common.Repositories;
using SocialImageSharing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialImageSharing.Data
{
	public interface ISocialImageSharingData
	{
		IDeletableEntityRepository<User> Users { get; }

		IDeletableEntityRepository<Post> Posts { get; }

		IDeletableEntityRepository<PostComment> PostComments { get; }

		IDeletableEntityRepository<PostLike> PostLikes { get; }

		IDeletableEntityRepository<CommentLike> CommentLikes { get; }

		IDeletableEntityRepository<UserFavoritePost> UserFavoritePosts { get; }

		int SaveChanges();
	}
}
