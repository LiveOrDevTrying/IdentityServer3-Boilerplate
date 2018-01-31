using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static IdentityServer3.Core.Constants;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IS3.Factories
{
    public class ClaimsIdentityFactory : ClaimsIdentityFactory<IdentityUser, string>
    {
        public ClaimsIdentityFactory()
        {
            UserIdClaimType = IdentityServer3.Core.Constants.ClaimTypes.Subject;
            UserNameClaimType = IdentityServer3.Core.Constants.ClaimTypes.PreferredUserName;
            RoleClaimType = IdentityServer3.Core.Constants.ClaimTypes.Role;
        }

        public override async Task<ClaimsIdentity> CreateAsync(UserManager<IdentityUser, string> manager, IdentityUser user, string authenticationType)
        {
            var ci = await base.CreateAsync(manager, user, authenticationType);

            if (!string.IsNullOrWhiteSpace(user.UserName))
            {
                ci.AddClaim(new Claim(IdentityServer3.Core.Constants.ClaimTypes.PreferredUserName, user.UserName));
            }

            if (!string.IsNullOrWhiteSpace(user.Id))
            {
                ci.AddClaim(new Claim(IdentityServer3.Core.Constants.ClaimTypes.Id, user.Id));
            }

            return ci;
        }
    }
}
