using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using Microsoft.EntityFrameworkCore;
using Players.Service.Events;
using Players.Service.Repositories;

namespace Players.Service.Commands.Handlers
{
    public class AssignToGroupHandler : ICommandHandler<AssignToGroupCommand>
    {
        private readonly PlayersDbContext _dbContext;
        private readonly IBusPublisher _busPublisher;

        public AssignToGroupHandler(PlayersDbContext context, IBusPublisher busPublisher)
        {
            _dbContext = context;
            _busPublisher = busPublisher;
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
            var @event = new PlayerAssignedToGroupEvent()
            {
                PlayerId = command.PlayerId,
                GroupId = command.GroupId
            };
            _busPublisher.PublishAsync(@event);

            return Task.CompletedTask;
        }
    }
}
