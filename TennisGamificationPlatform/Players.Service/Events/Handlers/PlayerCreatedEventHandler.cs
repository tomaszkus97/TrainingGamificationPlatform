using System;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Players.Service.Domain;
using Players.Service.Events.External;
using Players.Service.Repositories;

namespace Players.Service.Events.Handlers
{
    public class PlayerCreatedEventHandler : IEventHandler<PlayerCreatedEvent>
    {
        private readonly PlayersDbContext _dbContext;

        public PlayerCreatedEventHandler(PlayersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task HandleAsync(PlayerCreatedEvent @event)
        {
            var newPlayer = new Player()
            {
                Id = @event.PlayerId,
                IdentityId = new System.Guid(),
                Name = @event.Name,
                Surname = @event.Surname,
                Age = @event.Age,
                LevelId = 1
            };

            await _dbContext.Players.AddAsync(newPlayer);

            if (_dbContext.SaveChanges() == 0)
            {
                throw new Exception("Could not add new player");
            }
        }
    }
}
