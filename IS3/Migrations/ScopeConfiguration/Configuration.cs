namespace IS3.Migrations.ScopeConfiguration
{
    using IdentityServer3.Core.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IdentityServer3.EntityFramework.ScopeConfigurationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ScopeConfiguration";
        }

        protected override void Seed(IdentityServer3.EntityFramework.ScopeConfigurationDbContext context)
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

            //Providing standard scopes
            foreach (var standardScope in StandardScopes.All)
            {
                if (standardScope.Name == "openid")
                {
                    standardScope.Claims = new List<ScopeClaim>()
                    {
                        new ScopeClaim
                        {
                            AlwaysIncludeInIdToken = true,
                            Name = "access_token",
                            Description = "The Provider's access token"
                        },
                        new ScopeClaim
                        {
                            AlwaysIncludeInIdToken = true,
                            Name = "refresh_token",
                            Description = "The Provider's refresh token"
                        },
                        new ScopeClaim
                        {
                            AlwaysIncludeInIdToken = true,
                            Name = "userId",
                            Description = "The Provider's UserId"
                        }
                    };
                }

                if (!context.Scopes.Any(s => s.Name == standardScope.Name))
                    context.Scopes.Add(standardScope.ToEntity());
            }

            // Provide additional scopes here
        }
    }
}
