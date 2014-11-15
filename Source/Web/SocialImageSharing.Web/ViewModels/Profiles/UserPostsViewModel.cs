using AutoMapper;
using SocialImageSharing.Data.Models;
using SocialImageSharing.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialImageSharing.Web.ViewModels.Profiles
{
	public class UserPostsViewModel : IMapFrom<User>, IHaveCustomMappings
	{
		public string Id { get; set; }

		public string UserName { get; set; }

		public int NumberOfPosts { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<User, UserPostsViewModel>()
				.ForMember(dest => dest.NumberOfPosts,
					opt => opt.MapFrom(src => src.Posts.Count()));
		}
	}
}