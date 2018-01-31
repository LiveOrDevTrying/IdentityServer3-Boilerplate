using Microsoft.AspNet.Identity.EntityFramework;

namespace IS3.Contexts
{
    public class RoleStore : RoleStore<IdentityRole>
    {
        public RoleStore(IdentityContext context) : base(context)
        { }
    }
}