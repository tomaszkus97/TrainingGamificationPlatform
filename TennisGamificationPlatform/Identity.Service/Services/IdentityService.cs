using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Convey.MessageBrokers;
using Identity.Service.Contexts;
using Identity.Service.Domain;
using Identity.Service.Events;
using Identity.Service.Helpers;
using Identity.Service.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Service.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IdentityContext _dbContext;
        private readonly AppSettings _appSettings;
        private readonly IBusPublisher _publisher;

        public IdentityService(IOptions<AppSettings> appSettings, IBusPublisher publisher, IdentityContext dbContext)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
            _publisher = publisher;
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
        public async Task SignUpCoach(SignUpCoachModel model)
        {
            var suModel = new SignUpModel()
            {
                Username = model.Username,
                Password = model.Password,
                Role = Role.Coach.ToString()
            };
            var id = await SignUp(suModel);

            var @event = new CoachCreatedEvent()
            {
                CoachId = id,
                Name = model.Name,
                Surname = model.Surname
            };

            await _publisher.PublishAsync(@event);
        }

        public async Task SignUpPlayer(SignUpPlayerModel model)
        {
            var suModel = new SignUpModel()
            {
                Username = model.Username,
                Password = model.Password,
                Role = Role.Player.ToString()
            };
            var id = await SignUp(suModel);

            var @event = new PlayerCreatedEvent()
            {
                PlayerId = id,
                Name = model.Name,
                Surname = model.Surname,
                Age = model.Age,
                LevelName = model.LevelName
            };

            await _publisher.PublishAsync(@event);
        }


        private Task<Guid> SignUp(SignUpModel model)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Username == model.Username);
            if (user != null)
                throw new Exception("Username already in use");

            user = new User(model.Username, model.Password, model.Role);
            _dbContext.Users.Add(user);

            if (_dbContext.SaveChanges() == 0)
            {
                throw new Exception("Could not save user in database");
            }

            return Task.FromResult(user.Id);
        }
    }
}
