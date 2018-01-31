using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework;
using IS3.Contexts;
using IS3.Managers;
using IS3.Service;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Owin.Security.Providers.Twitch;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartupAttribute(typeof(IS3.Startup))]
namespace IS3
{
    public partial class Startup
    {
        public const string SITE_NAME = "<Your site name>";
        public const string REGISTER_URL = "<Your site Register URL>";
        public const string FORGOT_PASSWORD_URL = "<Your site Forgot Password URL>";

        public const string GOOGLE_CLIENT_ID = "<Google Client ID>";
        public const string GOOGLE_CLIENT_SECRET = "<Google Client Secret>";
        public const string TWITCH_CLIENT_ID = "<Twitch Client Id>";
        public const string TWITCH_CLIENT_SECRET = "<Twitch Client Secret>";

        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            var efConfig = new EntityFrameworkServiceOptions
            {
                ConnectionString = "DefaultConnection",
            };

            // Creating the factory
            var factory = new IdentityServerServiceFactory();

            factory.RegisterClientStore(efConfig);
            factory.RegisterScopeStore(efConfig);
            factory.RegisterOperationalServices(efConfig);

            factory.Register(new Registration<UserManager>());
            factory.Register(new Registration<UserStore>());
            factory.Register(new Registration<IdentityContext>());

            factory.CorsPolicyService = new Registration<ICorsPolicyService>(new PolicyService());

            factory.UserService = new Registration<IUserService, UserService>();

            // This is for Token CleanUp
            var cleanup = new TokenCleanup(efConfig, 60);
            cleanup.Start();
            app.UseIdentityServer(new IdentityServerOptions
            {
                SiteName = $"Log into {SITE_NAME}",
                SigningCertificate = Helper.LoadCertificate(),
                Factory = factory,
                AuthenticationOptions = new AuthenticationOptions
                {
                    EnablePostSignOutAutoRedirect = true,
                    EnableSignOutPrompt = true,
                    IdentityProviders = ConfigureIdentityProviders,
                    RememberLastUsername = true,
                    RequireSignOutPrompt = false,
                    LoginPageLinks = new LoginPageLink[]
                    {
                        new LoginPageLink
                        {
                            Text = $"Register for {SITE_NAME}",
                            Href = REGISTER_URL
                        },
                        new LoginPageLink
                        {
                            Text = "Forgot your password?",
                            Href = FORGOT_PASSWORD_URL
                        }
                    }
                },
                //EnableWelcomePage = false
            });
        }

        private void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            ConfigureGoogle(app, signInAsType);
            ConfigureTwitch(app, signInAsType);
        }

        private void ConfigureGoogle(IAppBuilder app, string signInAsType)
        {
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                AuthenticationType = "Google",
                Caption = "Google",
                SignInAsAuthenticationType = signInAsType,
                ClientId = GOOGLE_CLIENT_ID,
                ClientSecret = GOOGLE_CLIENT_SECRET,
                Provider = new GoogleOAuth2AuthenticationProvider
                {
                    OnAuthenticated = async n =>
                    {
                        n.Identity.AddClaim(new Claim(IdentityServer3.Core.Constants.ClaimTypes.Id, n.Id));
                        n.Identity.AddClaim(new Claim("access_token", n.AccessToken));
                        n.Identity.AddClaim(new Claim(IdentityServer3.Core.Constants.ClaimTypes.PreferredUserName, n.Name));
                        n.Identity.AddClaim(new Claim(IdentityServer3.Core.Constants.ClaimTypes.Role, "Authorize_Google"));
                        await Task.FromResult(0);
                    }
                }
            });
        }

        private void ConfigureTwitch(IAppBuilder app, string signInAsType)
        {
            app.UseTwitchAuthentication(new TwitchAuthenticationOptions()
            {
                AuthenticationType = "Twitch",
                Caption = "Twitch",
                SignInAsAuthenticationType = signInAsType,
                CallbackPath = new PathString("/signin-twitch"),
                ClientId = TWITCH_CLIENT_ID,
                ClientSecret = TWITCH_CLIENT_SECRET,
                ForceVerify = true,
                Provider = new TwitchAuthenticationProvider
                {
                    OnAuthenticated = async n =>
                    {
                        n.Identity.AddClaim(new Claim(IdentityServer3.Core.Constants.ClaimTypes.Id, n.Id));
                        n.Identity.AddClaim(new Claim("access_token_twitch", n.AccessToken));
                        n.Identity.AddClaim(new Claim(IdentityServer3.Core.Constants.ClaimTypes.Role, "Authorize_Twitch"));
                        n.Identity.AddClaim(new Claim("id_token", n.Id));
                        await Task.FromResult(0);
                    },
                }
            });
        }
    }
}
