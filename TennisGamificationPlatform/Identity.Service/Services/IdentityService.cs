using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity.Service.Contexts;
using Identity.Service.Domain;
using Identity.Service.Helpers;
using Identity.Service.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Service.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IdentityContext _dbContext;
        private readonly AppSettings _appSettings;

        public IdentityService(IOptions<AppSettings> appSettings, IdentityContext dbContext)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
        }

        public Task<string> SignIn(SignInModel model)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null)
                throw new Exception("Invalid credentials");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new[]
                            {new Claim("Role", user.Role.ToString()) })
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);



            return Task.FromResult(tokenHandler.WriteToken(token));
        }

        public Task SignUp(SignUpModel model)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Username == model.Username);
            if (user != null)
                throw new Exception("Username already in use");
            
            user = new User(model.Username,model.Password,model.Role);
            _dbContext.Users.Add(user);

           if(_dbContext.SaveChanges() == 0)
            {
                throw new Exception("Could not save user in database");
            }

            return Task.CompletedTask;
        }
    }
}
