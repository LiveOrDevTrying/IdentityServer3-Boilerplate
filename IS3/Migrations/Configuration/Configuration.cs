namespace IS3.Migrations.Configuration
{
    using IS3.Contexts;
    using IS3.Managers;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class Configuration : DbMigrationsConfiguration<IS3.Contexts.IdentityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Configuration";
        }

        protected override void Seed(IS3.Contexts.IdentityContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // This is for an Admin account
            if (!context.Users.Any(s => s.UserName == "Admin"))
            {
                using (var userManager = new UserManager(new UserStore(context)))
                {
                    var continueFlag = false;

                    Task.Run(new Action(async () =>
                    {
                        var user = new IdentityUser
                        {
                            UserName = "Admin",
                            Email = "admin@email.mom",
                            EmailConfirmed = true,
                        };

                        // password1 is the default admin password
                        var result = await userManager.CreateAsync(user, "password1");

                        if (result.Succeeded)
                        {
                            result = await userManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim(
                                IdentityServer3.Core.Constants.ClaimTypes.Role, "Admin"));
                            await context.SaveChangesAsync();
                        };

                        continueFlag = true;
                    }));

                    while (!continueFlag)
                    {
                        Thread.Sleep(200);
                    }
                }
            };
        }
    }
}
