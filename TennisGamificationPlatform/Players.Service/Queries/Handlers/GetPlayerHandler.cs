using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Convey.CQRS.Queries;
using Microsoft.EntityFrameworkCore;
using Players.Service.Dtos;
using Players.Service.Repositories;

namespace Players.Service.Queries.Handlers
{
    public class GetPlayerHandler : IQueryHandler<GetPlayerQuery, PlayerDto>
    {
        private readonly PlayersDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetPlayerHandler(PlayersDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public Task<PlayerDto> HandleAsync(GetPlayerQuery query)
        {
            var player = _dbContext.Players
                .Include(p=>p.CurrentLevel)
                .FirstOrDefault(p => p.Id == query.PlayerId);
            return Task.FromResult(_mapper.Map<PlayerDto>(player));
        }
    }
}
