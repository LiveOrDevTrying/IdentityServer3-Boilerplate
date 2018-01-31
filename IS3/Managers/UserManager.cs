using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using IS3.Contexts;

namespace IS3.Managers
{
    public class UserManager : UserManager<IdentityUser, string>
    {
        public UserManager(UserStore userStore) : base(userStore)
        {
        }
    }
}