using AutoMapper;
using SocialImageSharing.Data.Models;
using SocialImageSharing.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialImageSharing.Web.ViewModels.Posts
{
	public class PostDetailsViewModel : PostViewModel
	{
		public int LikeValue { get; set; }

		public bool IsInFavorites { get; set; }

		public ICollection<PostComment> Comments { get; set; }

		public override void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<Post, PostDetailsViewModel>()
				.ForMember(dest => dest.CreatorName,
					opt => opt.MapFrom(src => src.Creator.UserName))
				.ForMember(dest => dest.CreatorId,
					opt => opt.MapFrom(src => src.Creator.Id))
				.ForMember(dest => dest.Rating,
					opt => opt.MapFrom(src => src.Likes.Count(l => l.Value == 1) - src.Likes.Count(l => l.Value == -1)));
		}
	}
}