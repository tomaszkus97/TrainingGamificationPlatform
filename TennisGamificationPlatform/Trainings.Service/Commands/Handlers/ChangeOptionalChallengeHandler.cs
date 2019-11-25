using System;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Trainings.Service.Repositories;

namespace Trainings.Service.Commands.Handlers
{
    public class ChangeOptionalChallengeHandler : ICommandHandler<ChangeOptionalChallengeCommand>
    {
        private readonly TrainingsDbContext _dbContext;

        public ChangeOptionalChallengeHandler(TrainingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task HandleAsync(ChangeOptionalChallengeCommand command)
        {
            var group = _dbContext.TrainingGroups.FirstOrDefault(tg => tg.Id == command.GroupId);
            if(group != null)
            {
                group.SetOptionalChallenge(command.ChallengeId);
            }
            else
            {
                throw new Exception("Requested group does not exist");
            }

            return Task.CompletedTask;
        }
    }
}
