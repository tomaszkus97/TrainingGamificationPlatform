using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Players.Service.Domain;
using Players.Service.Dtos;

namespace Players.Service.Common
{
    public class PlayersServiceMappings : Profile
    {
        public PlayersServiceMappings()
        {
            CreateMap<Player, PlayerDto>();
        }
    }
}
