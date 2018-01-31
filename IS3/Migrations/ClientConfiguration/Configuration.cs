namespace IS3.Migrations.ClientConfiguration
{
    using IdentityServer3.Core.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IdentityServer3.EntityFramework.ClientConfigurationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ClientConfiguration";
        }

        protected override void Seed(IdentityServer3.EntityFramework.ClientConfigurationDbContext context)
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

            if (!context.Clients.Any(s => s.ClientId == "ClientID"))
            {
                var client = new Client
                {
                    ClientName = "ClientName",
                    ClientId = "ClientID",
                    Flow = Flows.ClientCredentials,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("ClientSecret".Sha256()),
                    },
                    AllowedScopes = new List<string>
                    {
                        "Scopes"
                    }
                };

                context.Clients.Add(client.ToEntity());
                context.SaveChanges();
            }

            if (!context.Clients.Any(s => s.ClientId == "ClientID2"))
            {
                var client = new Client
                {
                    ClientName = "ClientName",
                    ClientId = "ClientID",
                    Flow = Flows.Implicit,
                    AllowedScopes = new List<string>
                    {
                        "Scopes"
                    },
                    AllowAccessToAllScopes = false,
                    ClientUri = "ClientURI",
                    RedirectUris = new List<string>
                    {
                        "RedirectUris"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "PostLogoutRedirectsUris"
                    },
                    LogoutSessionRequired = false
                };

                context.Clients.Add(client.ToEntity());
                context.SaveChanges();
            }
        }
    }
}
