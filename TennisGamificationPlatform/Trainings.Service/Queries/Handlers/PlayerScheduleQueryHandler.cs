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
    public class PlayerScheduleQueryHandler : IQueryHandler<PlayerScheduleQuery, IEnumerable<ScheduledTrainingDto>>
    {
        private readonly TrainingsDbContext _dbContext;

        public PlayerScheduleQueryHandler(TrainingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ScheduledTrainingDto>> HandleAsync(PlayerScheduleQuery query)
        {
            var groups = await _dbContext.TrainingGroups.Include(g => g.Coach)
                .Include(g=>g.PlayerTrainingGroups)
                .Where(g => g.PlayerTrainingGroups.Any(ptg => ptg.PlayerId == query.PlayerId))
                .Select(g => new ScheduledTrainingDto()
                {
                    GroupName = g.Name,
                    CoachName = $"{g.Coach.Name} {g.Coach.Surname}",
                    LevelName = g.LevelName,
                    Day = g.Day,
                    Hour = g.Hour
                })
                .OrderByDescending(t => t.Day)
                .ThenBy(t => t.Hour)
                .ToListAsync();

            return groups;
        }
    }
}
