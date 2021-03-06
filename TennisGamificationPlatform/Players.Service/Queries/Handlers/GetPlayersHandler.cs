﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Convey.CQRS.Queries;
using Microsoft.EntityFrameworkCore;
using Players.Service.Domain;
using Players.Service.Dtos;
using Players.Service.Repositories;

namespace Players.Service.Queries.Handlers
{
    public class GetPlayersHandler : IQueryHandler<GetPlayersQuery, IEnumerable<PlayerDto>>
    {
        private readonly PlayersDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetPlayersHandler(PlayersDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        
        public Task<IEnumerable<PlayerDto>> HandleAsync(GetPlayersQuery query)
        {
            var players = _dbContext.Players.Include(p => p.AssignedGroups).ToList();
            if (query.PlayerId.Count() != 0)
            {
                players = players.Where(p => query.PlayerId.Contains(p.Id)).ToList();
            }
            return Task.FromResult(_mapper.Map<IEnumerable<PlayerDto>>(players));
        }
    }
}
