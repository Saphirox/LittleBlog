namespace LittleBlog.DAL.Migrations
{
    using LittleBlog.DAL.Identity;
    using LittleBlog.Entities.Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LittleBlog.DAL.Persistence.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LittleBlog.DAL.Persistence.Context context)
        {
            var userManager = new AppUserManager(new UserStore<AppUser>(context));

            var roleManager = new AppRoleManager(new RoleStore<AppRole>(context));

            roleManager.Create(new AppRole { Name = "User" });
            roleManager.Create(new AppRole { Name = "Admin" });

            var adminName = "suprmaks@gmail.com";
            var adminPassword = "123456";

            var user = new AppUser()
            {
                UserName = adminName,
                Email = adminName
            };

            userManager.Create(user, adminPassword);
            userManager.AddToRole(user.Id, "Admin");
        }
    }
}
