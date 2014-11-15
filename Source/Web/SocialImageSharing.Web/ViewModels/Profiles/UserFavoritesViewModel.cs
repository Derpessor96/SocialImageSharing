using AutoMapper;
using SocialImageSharing.Data.Models;
using SocialImageSharing.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialImageSharing.Web.ViewModels.Profiles
{
	public class UserFavoritesViewModel : IMapFrom<User>, IHaveCustomMappings
	{

		public string Id { get; set; }

		public string UserName { get; set; }

		public int NumberOfFavorites { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<User, UserFavoritesViewModel>()
				.ForMember(dest => dest.NumberOfFavorites,
					opt => opt.MapFrom(src => src.Favorites.Count(f => f.IsDeleted == false && f.Post.IsDeleted == false)));
		}
	}
}