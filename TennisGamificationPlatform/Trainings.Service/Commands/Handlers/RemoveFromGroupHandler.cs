using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.EntityFrameworkCore;
using Trainings.Service.Repositories;

namespace Trainings.Service.Commands.Handlers
{
    public class RemoveFromGroupHandler : ICommandHandler<RemoveFromGroupCommand>
    {
        private readonly TrainingsDbContext _dbContext;

        public RemoveFromGroupHandler(TrainingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task HandleAsync(RemoveFromGroupCommand command)
        {
            var group = _dbContext.TrainingGroups.Include(tg => tg.PlayerTrainingGroups)
                .FirstOrDefault(tg => tg.Id == command.GroupId);

            if (group != null)
            {
                group.RemovePlayer(command.PlayerId);
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

