using AutoMapper;
using SocialImageSharing.Data.Common.Models;
using SocialImageSharing.Data.Models;
using SocialImageSharing.Web.Areas.Administration.ViewModels.Base;
using SocialImageSharing.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialImageSharing.Web.Areas.Administration.ViewModels.Posts
{
	public class PostViewModel : AdministrationViewModel, IMapFrom<Post>, IHaveCustomMappings
	{
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Description { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string CreatorName { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<Post, PostViewModel>()
				.ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator.UserName));
		}
	}
}