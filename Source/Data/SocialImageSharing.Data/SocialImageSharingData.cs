using SocialImageSharing.Data.Common.Models;
using SocialImageSharing.Data.Common.Repositories;
using SocialImageSharing.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialImageSharing.Data
{
	public class SocialImageSharingData : ISocialImageSharingData
	{
		private DbContext context;
		private IDictionary<Type, object> repositories;

		public SocialImageSharingData(DbContext context)
		{
			this.context = context;
			this.repositories = new Dictionary<Type, object>();
		}

		public SocialImageSharingData()
			: this(new SocialImageSharingDbContext())
		{
		}

		public IDeletableEntityRepository<User> Users
		{
			get
			{
				return this.GetRepository<User>();
			}
		}

		public int SaveChanges()
		{
			return this.context.SaveChanges();
		}

		private IDeletableEntityRepository<T> GetRepository<T>() where T : class, IDeletableEntity
		{
			var typeOfRepository = typeof(T);

			if (!this.repositories.ContainsKey(typeOfRepository))
			{
				var newRepository = Activator.CreateInstance(typeof(DeletableEntityRepository<T>), this.context);
				this.repositories.Add(typeOfRepository, newRepository);

			}

			return (IDeletableEntityRepository<T>)this.repositories[typeOfRepository];
		}
	}
}
