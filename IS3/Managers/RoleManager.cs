using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using IS3.Contexts;

namespace IS3.Managers
{
    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager(RoleStore store) : base(store)
        { }
    }
}