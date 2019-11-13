using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Service.Services
{
    public interface IClaimsProvider
    {
        Task<IDictionary<string, string>> Get(Guid userId);
    }
}
