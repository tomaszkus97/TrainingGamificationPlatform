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
    public class TodayGroupsQueryHandler : IQueryHandler<TodayGroupsQuery, IEnumerable<ScheduledTrainingDto>>
    {
        private readonly TrainingsDbContext _dbContext;
        private readonly IPlayersServiceClient _playersServiceClient;

        public TodayGroupsQueryHandler(TrainingsDbContext dbContext, IPlayersServiceClient playersServiceClient)
        {
            _dbContext = dbContext;
            _playersServiceClient = playersServiceClient;
        }
      
        public async Task<IEnumerable<ScheduledTrainingDto>> HandleAsync(TodayGroupsQuery query)
        {
            var today = DateTime.Now.DayOfWeek;
            var groups = await _dbContext.TrainingGroups.Include(tg => tg.PlayerTrainingGroups)
                .Where(g => g.Day == today).Select(g => new ScheduledTrainingDto()
                {
                    GroupId = g.Id.ToString(),
                    GroupName = g.Name,
                    CoachName = $"{g.Coach.Name} {g.Coach.Surname}",
                    LevelName = g.LevelName,
                    Day = g.Day,
                    Hour = g.Hour.ToString(),
                    Players = g.PlayerTrainingGroups.Select(ptg => new PlayerDto()
                    {
                        Id = ptg.PlayerId
                    })
                }).ToListAsync();

            foreach (var group in groups)
            {
                var playerIds = group.Players.Select(g => g.Id).ToList();
                if (playerIds.Count == 0)
                {
                    group.Players = new List<PlayerDto>();
                }
                else
                {
                    var players = await _playersServiceClient.GetPlayers(playerIds);
                    group.Players = players.ToList().Select(p => new PlayerDto()
                    {
                        Name = p.Name,
                        Surname = p.Surname,
                        Age = p.Age,
                        LevelId = p.LevelId,
                        Points = p.Points,
                        Id = p.Id
                    });
                }

            }

            return groups;
        }
    }
}
