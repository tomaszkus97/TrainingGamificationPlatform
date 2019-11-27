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
    public class GroupAttendanceQueryHandler : IQueryHandler<GroupAttendanceQuery, IEnumerable<GroupAttendanceDto>>
    {
        private readonly TrainingsDbContext _dbContext;

        public GroupAttendanceQueryHandler(TrainingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<GroupAttendanceDto>> HandleAsync(GroupAttendanceQuery query)
        {
            var dateFrom = !string.IsNullOrEmpty(query.DateFrom) ? DateTime.Parse(query.DateFrom)
                : DateTime.MinValue;
            var dateTo = !string.IsNullOrEmpty(query.DateTo) ? DateTime.Parse(query.DateTo)
                : DateTime.MaxValue;
            var groupAttendances = _dbContext.Attendances.Include(a=>a.AttendantPlayers)
                .Where(a => a.GroupId == query.GroupId
                && a.Date >= dateFrom && a.Date <= dateTo)
                .Select(a => new GroupAttendanceDto()
                {
                    Date = a.Date.ToShortDateString(),
                    GroupId = query.GroupId,
                    AttendantPlayers = a.AttendantPlayers.Select(ap => ap.PlayerId)
                });

            return await groupAttendances.ToListAsync();
        }
    }
}
