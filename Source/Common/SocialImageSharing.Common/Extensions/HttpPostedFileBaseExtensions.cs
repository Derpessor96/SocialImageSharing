using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SocialImageSharing.Common.Extensions
{
	public static class HttpPostedFileBaseExtensions
	{
		public static string GetExtension(this HttpPostedFileBase file)
		{
			string name = file.FileName;

			if (name.IndexOf('.') == -1)
			{
				return null;
			}

			return name.Substring(name.LastIndexOf('.') + 1);
		}
	}
}
