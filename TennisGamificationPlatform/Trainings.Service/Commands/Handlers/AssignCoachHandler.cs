using System;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.EntityFrameworkCore;
using Trainings.Service.Repositories;

namespace Trainings.Service.Commands.Handlers
{
    public class AssignCoachHandler : ICommandHandler<AssignCoachCommand>
    {
        private readonly TrainingsDbContext _dbContext;

        public AssignCoachHandler(TrainingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task HandleAsync(AssignCoachCommand command)
        {
            var group = _dbContext.TrainingGroups.Include(tg => tg.PlayerTrainingGroups)
                .FirstOrDefault(tg => tg.Id == command.GroupId);
            var coach = _dbContext.Coaches.FirstOrDefault(c => c.Id == command.CoachId);
            if(coach == null)
            {
                throw new Exception("Requested coach does not exist");
            }

            if (group != null)
            {
                group.SetCoach(command.CoachId);
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
