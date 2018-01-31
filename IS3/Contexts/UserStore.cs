using Microsoft.AspNet.Identity.EntityFramework;

namespace IS3.Contexts
{
    public class UserStore : UserStore<IdentityUser>
    {
        public UserStore(IdentityContext context) : base(context)
        { }
    }
}