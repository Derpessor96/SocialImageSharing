namespace SocialImageSharing.Data.Migrations
{
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
        }
    }
}
