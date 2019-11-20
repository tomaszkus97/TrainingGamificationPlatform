using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.EntityFrameworkCore;
using Players.Service.Repositories;

namespace Players.Service.Commands.Handlers
{
    public class AssignToGroupHandler : ICommandHandler<AssignToGroupCommand>
    {
        private readonly PlayersDbContext _dbContext;

        public AssignToGroupHandler(PlayersDbContext context)
        {
            _dbContext = context;
        }

        public Task HandleAsync(AssignToGroupCommand command)
        {
            var player = _dbContext.Players.Include(p=>p.AssignedGroups).FirstOrDefault(p => p.Id == command.PlayerId);


            if (player == null)
            {
                throw new Exception("Cannot find player");
            }
            if (player.AssignedGroups.Any(g => g.GroupId == command.GroupId))
            {
                throw new Exception("Player was already assigned to that group");
            }
            player.AssignToGroup(command.GroupId);

            if (_dbContext.SaveChanges() == 0)
            {
                throw new Exception("Could not assign player to group");
            }

            return Task.CompletedTask;
        }
    }
}
