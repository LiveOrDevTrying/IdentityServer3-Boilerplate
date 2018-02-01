# IdentityServer3 - Boilerplate
A generic implementation of IdentityServer3 including the Asp.Net Identity System, Roles, and Entity Framework extensions.

## To implement:
1. Change connection string "DefaultConnection" to your SQL database
2. Run the following Nuget commands to generate migrations. Adjust naming if required:
   * Enable-Migrations -MigrationsDirectory Migrations\ClientConfiguration -ContextTypeName ClientConfigurationDbContext -ContextAssemblyName IdentityServer3.EntityFramework -ConnectionStringName DefaultConnection
   * Enable-Migrations -MigrationsDirectory Migrations\ScopeConfiguration -ContextTypeName ScopeConfigurationDbContext -ContextAssemblyName IdentityServer3.EntityFramework -ConnectionStringName DefaultConnection
   * Enable-Migrations -MigrationsDirectory Migrations\OperationalConfiguration -ContextTypeName OperationalDbContext -ContextAssemblyName IdentityServer3.EntityFramework -ConnectionStringName DefaultConnection
   * Enable-Migrations -MigrationsDirectory Migrations\Configuration -ConnectionStringName DefaultConnection

2. Seed the configuration.cs files within the Migrations folder with your clients' information. This includes:
   * ClientConfiguration - Information related to your client applications
   * ScopeConfiguration - Scopes related to your client applications and permissions
   * Configuration - Default administrative account configuration

3. Change all settings within Startup.CS to match your 3rd party credentials and specific site name.

4. Change claims factory (ClaimsIdentityFactory.cs) for custom claims, if needed.

5. Run the following Nuget commands to add the initial migrations:
   * Add-Migration -Name InitialCreate -ConfigurationTypeName IS3.Migrations.ScopeConfiguration.Configuration -ConnectionStringName DefaultConnection
   * Add-Migration -Name InitialCreate -ConfigurationTypeName IS3.Migrations.ClientConfiguration.Configuration -ConnectionStringName DefaultConnection
   * Add-Migration -Name InitialCreate -ConfigurationTypeName IS3.Migrations.OperationalConfiguration.Configuration -ConnectionStringName DefaultConnection
   * Add-Migration -Name InitialCreate -ConfigurationTypeName IS3.Migrations.Configuration.Configuration -ConnectionStringName DefaultConnection

6. Run the following Nuget commands to update the database:
   * Update-Database -ConfigurationTypeName IS3.Migrations.ClientConfiguration.Configuration -ConnectionStringName DefaultConnection
   * Update-Database -ConfigurationTypeName IS3.Migrations.ScopeConfiguration.Configuration -ConnectionStringName DefaultConnection
   * Update-Database -ConfigurationTypeName IS3.Migrations.OperationalConfiguration.Configuration -ConnectionStringName DefaultConnection
   * Update-Database -ConfigurationTypeName IS3.Migrations.Configuration.Configuration -ConnectionStringName DefaultConnection


[IdentityServer3 documentation is located here.](https://identityserver.github.io/Documentation/docsv2/ "IdentityServer3 Documentation")

[IdentityServer3 Entity-Framework extension.](https://github.com/IdentityServer/IdentityServer3.EntityFramework "IdentityServer3 EntityFramework Extension")

[IdentityServer3 AspNet-Identity extension.](https://github.com/IdentityServer/IdentityServer3.AspNetIdentity "AspNet-Identity")

[OAuth 2.0 Protocol.](https://tools.ietf.org/html/rfc6749 "OAuth 2.0 Protocol")
