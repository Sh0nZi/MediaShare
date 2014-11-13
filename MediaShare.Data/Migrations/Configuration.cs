namespace MediaShare.Data.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MediaShare.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MediaShare.Common;
    
    internal sealed class Configuration : DbMigrationsConfiguration<MediaShareContext>
    {
        private UserManager<ApplicationUser> userManager;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;

            //TO DO: Set to false
            this.AutomaticMigrationDataLossAllowed = true;
            ContextKey = "MediaShare.Data.MediaShareContext";
        }

        protected override void Seed(MediaShareContext context)
        {
            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            this.SeedRoles(context);
            this.SeedAdmin(context);
        }

        private void SeedRoles(MediaShareContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalConstants.Admin));
            context.SaveChanges();
        }

        private void SeedAdmin(MediaShareContext context)
        {
            if (!context.Users.Any()) { 
            var user = new ApplicationUser
            {
                Email = "Admin@admin.com",
                UserName = "Admin@admin.com"
            };
            this.userManager.Create(user, "123456");
            this.userManager.AddToRole(user.Id, GlobalConstants.Admin);
                }
        }
    }
}