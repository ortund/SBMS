namespace SBMSAdmin.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SBMSAdmin.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SBMSAdmin.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SBMSAdmin.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("global"))
            {
                var role = new IdentityRole { Name = "global" };
                roleManager.Create(role);

                var user = new ApplicationUser { UserName = "logany", Email = "logan@euphoria.systems" };

                string pwd = "Moomin@87088";
                var chkUser = userManager.Create(user, pwd);

                if (chkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "global");
                }
            }
        }
    }
}
