using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Trainings.Service.Domain;
using Trainings.Service.Repositories;

namespace Trainings.Service.Commands.Handlers
{
    public class CreateGroupHandler : ICommandHandler<CreateGroupCommand>
    {
        private readonly TrainingsDbContext _dbContext;

        public CreateGroupHandler(TrainingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task HandleAsync(CreateGroupCommand command)
        {
            var group = new TrainingGroup(command.Day, command.Hour, command.LevelName);
            group.SetCoach(command.CoachId);
            _dbContext.TrainingGroups.Add(group);
            if (_dbContext.SaveChanges() == 0)
            {
                throw new Exception("Could not add created group");
            }

            return Task.CompletedTask;
        }
    }
}
