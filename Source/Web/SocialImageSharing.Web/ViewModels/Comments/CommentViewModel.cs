using AutoMapper;
using SocialImageSharing.Data.Models;
using SocialImageSharing.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialImageSharing.Web.ViewModels.Comments
{
	public class CommentViewModel : IMapFrom<PostComment>, IHaveCustomMappings
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		[AllowHtml]
		public string Content { get; set; }

		public string CreatorId { get; set; }

		public string CreatorName { get; set; }

		public int PostId { get; set; }

		public DateTime CreatedOn { get; set; }

		public int Rating { get; set; }

		public int LikeValue { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<PostComment, CommentViewModel>()
				.ForMember(dest => dest.CreatorName,
					opt => opt.MapFrom(src => src.User.UserName))
				.ForMember(dest => dest.CreatorId,
					opt => opt.MapFrom(src => src.User.Id))
				.ForMember(dest => dest.Rating,
					opt => opt.MapFrom(src => src.Likes.Count(l => l.Value == 1) - src.Likes.Count(l => l.Value == -1)));
		}
	}
}