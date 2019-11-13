using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Service.Contexts;

namespace Identity.Service.Services
{
    public class ClaimsProvider : IClaimsProvider
    {
        private readonly IdentityContext _dbContext;

        public ClaimsProvider(IdentityContext context)
        {
            _dbContext = context;
        }
        public async Task<IDictionary<string, string>> Get(Guid userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            var claims = new Dictionary<string, string>();
            if (user != null)
            {
                claims.Add("Role", user.Role.ToString());
            }

            return await Task.FromResult(claims);
        }
    }
}
