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
    public class GetGroupsByIdQueryHandler : IQueryHandler<GetGroupsByIdQuery, IEnumerable<ScheduledTrainingDto>>
    {
        private readonly TrainingsDbContext _dbContext;

        public GetGroupsByIdQueryHandler(TrainingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ScheduledTrainingDto>> HandleAsync(GetGroupsByIdQuery query)
        {
            var groups = _dbContext.TrainingGroups.Include(g => g.Coach).ToList();
            if (query.GroupIds.Any())
            {
                groups = groups.Where(g => query.GroupIds.Contains(g.Id)).ToList();
            }

            return groups.Select(g => new ScheduledTrainingDto
            {
                GroupName = g.Name,
                CoachName = $"{g.Coach.Name} {g.Coach.Surname}",
                LevelName = g.LevelName,
                Day = g.Day,
                Hour = g.Hour.ToString()
            }).ToList();

        }
    }
}
