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
    public class GetCoachesQueryHandler : IQueryHandler<GetCoachesQuery, IEnumerable<CoachDto>>
    {
        private readonly TrainingsDbContext _dbContext;

        public GetCoachesQueryHandler(TrainingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<CoachDto>> HandleAsync(GetCoachesQuery query)
        {
            return await _dbContext.Coaches.Select(c => new CoachDto()
            {
                Id = c.Id,
                Name = c.Name,
                Surname = c.Surname
            }).ToListAsync();
        }
    }
}
