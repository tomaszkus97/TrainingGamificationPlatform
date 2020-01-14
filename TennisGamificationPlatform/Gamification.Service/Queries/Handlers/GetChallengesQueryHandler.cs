using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Gamification.Service.Dtos;
using Gamification.Service.Repositories;

namespace Gamification.Service.Queries.Handlers
{
    public class GetChallengesQueryHandler : IQueryHandler<GetChallengesQuery, IEnumerable<ChallengeDto>>
    {
        private readonly GamificationDbContext _dbContext;

        public GetChallengesQueryHandler(GamificationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IEnumerable<ChallengeDto>> HandleAsync(GetChallengesQuery query)
        {
            var challenges = _dbContext.Challenges.ToList();
            if (query.ChallengeId.Count() != 0)
            {
                challenges = challenges.Where(c => query.ChallengeId.Contains(c.Id)).ToList();
            }
            var results = challenges.Select(c => new ChallengeDto()
            {
                Id = c.Id,
                Description = c.Description,
                IsObligatory = c.IsObligatory,
                LevelId = c.LevelId,
                Title = c.Title
            });
            return Task.FromResult(results);
        }
    }
}
