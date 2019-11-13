using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Players.Service.Domain;
using Players.Service.Repositories;

namespace Players.Service.Commands.Handlers
{
    public class ChangeLevelHandler : ICommandHandler<ChangeLevelCommand>
    {
        private readonly PlayersDbContext _dbContext;

        public ChangeLevelHandler(PlayersDbContext context)
        {
            _dbContext = context;
        }

       
           

        public Task HandleAsync(ChangeLevelCommand command)
        {
            var player = _dbContext.Players.FirstOrDefault(p => p.Id == command.PlayerId);

            if (player == null)
            {
                throw new Exception("Cannot find player");
            }

            player.CurrentLevel = new Level(command.LevelId, command.LevelName, command.PointsToAdvance);

            if (_dbContext.SaveChanges() == 0)
            {
                throw new Exception("Could not change player level");
            }

            return Task.CompletedTask;
        
    }
    }
}
