using Microsoft.AspNet.Identity.EntityFramework;
using SocialImageSharing.Data.Common.Models;
using SocialImageSharing.Data.Migrations;
using SocialImageSharing.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialImageSharing.Data
{
	public class SocialImageSharingDbContext : IdentityDbContext<User>
	{
		public SocialImageSharingDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<SocialImageSharingDbContext, Configuration>());
		}

		public static SocialImageSharingDbContext Create()
		{
			return new SocialImageSharingDbContext();
		}

		public override int SaveChanges()
		{
			this.ApplyAuditInfoRules();
			return base.SaveChanges();
		}

		private void ApplyAuditInfoRules()
		{
			foreach (var entry in
				this.ChangeTracker.Entries()
					.Where(
						e =>
						e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
			{
				var entity = (IAuditInfo)entry.Entity;

				if (entry.State == EntityState.Added)
				{
					if (!entity.PreserveCreatedOn)
					{
						entity.CreatedOn = DateTime.Now;
					}
				}
				else
				{
					entity.ModifiedOn = DateTime.Now;
				}
			}
		}
	}
}
