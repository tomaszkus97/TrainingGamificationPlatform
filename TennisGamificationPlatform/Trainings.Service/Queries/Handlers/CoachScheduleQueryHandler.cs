using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Microsoft.EntityFrameworkCore;
using Trainings.Service.Dtos;
using Trainings.Service.Repositories;
using Trainings.Service.Services.Clients;

namespace Trainings.Service.Queries.Handlers
{
    public class CoachScheduleQueryHandler : IQueryHandler<CoachScheduleQuery, IEnumerable<ScheduledTrainingDto>>
    {
        private readonly TrainingsDbContext _dbContext;
        private readonly IPlayersServiceClient _playersServiceClient;

        public CoachScheduleQueryHandler(TrainingsDbContext dbContext, IPlayersServiceClient playersServiceClient)
        {
            _dbContext = dbContext;
            _playersServiceClient = playersServiceClient;
        }

        public async Task<IEnumerable<ScheduledTrainingDto>> HandleAsync(CoachScheduleQuery query)
        {
            var groups = await _dbContext.TrainingGroups.Include(g => g.Coach).Include(g => g.PlayerTrainingGroups)
                .Where(g => g.CoachId == query.CoachId)
                .Select(g => new ScheduledTrainingDto()
                {
                    GroupName = g.Name,
                    CoachName = $"{g.Coach.Name} {g.Coach.Surname}",
                    LevelName = g.LevelName,
                    Day = g.Day,
                    Hour = g.Hour.ToString(),
                    Players =  _playersServiceClient.GetPlayers(g.PlayerTrainingGroups.Select(ptg => ptg.PlayerId)).Result
                })
                .OrderByDescending(t=>t.Day)
                .ThenBy(t=>t.Hour)
                .ToListAsync();

            return groups;
        }
    }
}
