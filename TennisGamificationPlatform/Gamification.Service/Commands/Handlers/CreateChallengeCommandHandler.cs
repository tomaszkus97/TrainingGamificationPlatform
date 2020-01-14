using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Gamification.Service.Domain;
using Gamification.Service.Repositories;

namespace Gamification.Service.Commands.Handlers
{
    public class CreateChallengeCommandHandler : ICommandHandler<CreateChallengeCommand>
    {
        private readonly GamificationDbContext _dbContext;
        public CreateChallengeCommandHandler(GamificationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task HandleAsync(CreateChallengeCommand command)
        {
            var challenge = new Challenge()
            {
                Id = Guid.NewGuid(),
                Description = command.Description,
                IsObligatory = command.IsObligatory,
                LevelId = command.LevelId,
                Title = command.Title
            };
            _dbContext.Challenges.Add(challenge);

            if (_dbContext.SaveChanges() == 0)
            {
                throw new Exception("Could not create challenge");
            }

            return Task.CompletedTask;
        }
    }
}
