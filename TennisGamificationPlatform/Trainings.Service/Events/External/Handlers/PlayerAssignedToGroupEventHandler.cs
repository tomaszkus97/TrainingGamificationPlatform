using System;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.EntityFrameworkCore;
using Trainings.Service.Domain;
using Trainings.Service.Events.External;
using Trainings.Service.Repositories;

namespace Trainings.Service.Commands.Handlers
{
    public class PlayerAssignedToGroupEventHandler : IEventHandler<PlayerAssignedToGroupEvent>
    {
        private readonly TrainingsDbContext _dbContext;

        public PlayerAssignedToGroupEventHandler(TrainingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task HandleAsync(PlayerAssignedToGroupEvent @event)
        {
            var group = _dbContext.TrainingGroups.Include(tg => tg.PlayerTrainingGroups)
                .FirstOrDefault(tg => tg.Id == @event.GroupId);

            var player = _dbContext.Players.FirstOrDefault(p => p.Id == @event.PlayerId);

            if(player == null)
            {
                var newPlayer = new Player()
                {
                    Id = @event.PlayerId
                };
                _dbContext.Players.Add(newPlayer);
            }

            if(group != null)
            {
                group.AssignPlayer(@event.PlayerId);
                if (_dbContext.SaveChanges() == 0)
                {
                    throw new Exception("Could not add player to group");
                }
            }
            else
            {
                throw new Exception("Requested group does not exist");
            }

            return Task.CompletedTask;
        }
    }
}
