using IdentityServer3.Core.Services;
using System.Threading.Tasks;

namespace IS3.Service
{
    public class PolicyService : ICorsPolicyService
    {
        public virtual async Task<bool> IsOriginAllowedAsync(string origin)
        {
            await Task.FromResult<object>(null);
            return true;
        }
    }
}