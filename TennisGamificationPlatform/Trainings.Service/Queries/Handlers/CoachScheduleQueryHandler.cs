using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Microsoft.EntityFrameworkCore;
using Trainings.Service.Dtos;
using Trainings.Service.Repositories;

namespace Trainings.Service.Queries.Handlers
{
    public class CoachScheduleQueryHandler : IQueryHandler<CoachScheduleQuery, IEnumerable<ScheduledTrainingDto>>
    {
        private readonly TrainingsDbContext _dbContext;

        public CoachScheduleQueryHandler(TrainingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ScheduledTrainingDto>> HandleAsync(CoachScheduleQuery query)
        {
            var groups = await _dbContext.TrainingGroups.Include(g => g.Coach)
                .Where(g => g.CoachId == query.CoachId)
                .Select(g => new ScheduledTrainingDto()
                {
                    GroupName = g.Name,
                    CoachName = $"{g.Coach.Name} {g.Coach.Surname}",
                    LevelName = g.LevelName
                }).ToListAsync();

            return groups;
        }
    }
}
