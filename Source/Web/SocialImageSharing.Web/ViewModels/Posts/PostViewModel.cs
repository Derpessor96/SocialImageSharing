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
	public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public DateTime CreatedOn { get; set; }

		public string CreatorName { get; set; }

		public int Rating { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<Post, PostViewModel>()
				.ForMember(dest => dest.CreatorName,
					opt => opt.MapFrom(src => src.Creator.UserName))
				.ForMember(dest => dest.Rating,
					opt => opt.MapFrom(src => src.Likes.Count(l => l.Value == 1) - src.Likes.Count(l => l.Value == -1)));
		}
	}
}