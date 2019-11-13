using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Service.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Service.Services
{
    public interface IIdentityService
    {
        Task SignUp(SignUpModel model);
        Task<string> SignIn(SignInModel model);
    }
}
