namespace SocialImageSharing.Data.Migrations
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using SocialImageSharing.Data.Models;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	public sealed class Configuration : DbMigrationsConfiguration<SocialImageSharingDbContext>
	{
		public Configuration()
		{
			this.AutomaticMigrationsEnabled = true;
			this.AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(SocialImageSharingDbContext context)
		{
			if (context.Users.Any())
			{
				return;
			}

			var manager = new UserManager<User>(new UserStore<User>(context));
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

			if (!roleManager.RoleExists("admin"))
			{
				roleManager.Create(new IdentityRole("admin"));
			}

			var result = manager.Create(new User() { UserName = "admin@admin.admin" }, password: "123456");
			var user = context.Users.First(u => u.UserName == "admin@admin.admin");

			manager.AddToRole(user.Id, "admin");
		}
	}
}
