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
    public class PlayerAttendanceQueryHandler : IQueryHandler<PlayerAttendanceQuery, IEnumerable<PlayerAttendanceDto>>
    {
        private readonly TrainingsDbContext _dbContext;

        public PlayerAttendanceQueryHandler(TrainingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PlayerAttendanceDto>> HandleAsync(PlayerAttendanceQuery query)
        {
            var dateFrom = !string.IsNullOrEmpty(query.DateFrom) ? DateTime.Parse(query.DateFrom) 
                : DateTime.MinValue;
            var dateTo = !string.IsNullOrEmpty(query.DateTo) ? DateTime.Parse(query.DateTo)
                : DateTime.MaxValue;
            var playerAttendances = _dbContext.PlayerAttendance.Include(pa => pa.Attendance).Where(p => p.PlayerId == query.PlayerId
                && p.Attendance.Date >= dateFrom && p.Attendance.Date <= dateTo)
                .Select(a => new PlayerAttendanceDto()
                {
                    Date = a.Attendance.Date.ToShortDateString(),
                    PlayerId = query.PlayerId,
                    GroupId = a.Attendance.GroupId
                });

            return await playerAttendances.ToListAsync();  
        }
    }
}
