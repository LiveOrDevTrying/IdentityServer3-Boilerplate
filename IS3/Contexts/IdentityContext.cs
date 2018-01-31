using Microsoft.AspNet.Identity.EntityFramework;

namespace IS3.Contexts
{
    public class IdentityContext : IdentityDbContext<IdentityUser>
    {
        public IdentityContext() : base("DefaultConnection")
        { }
    }
}