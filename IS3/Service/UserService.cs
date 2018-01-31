using IdentityServer3.AspNetIdentity;
using Microsoft.AspNet.Identity.EntityFramework;
using IS3.Managers;

namespace IS3.Service
{
    public class UserService : AspNetIdentityUserService<IdentityUser, string>
    {
        public UserService(UserManager userManager) : base(userManager)
        {

        }
    }
}