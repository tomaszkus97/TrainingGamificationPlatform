using System;
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
            CreateMap<PlayerGroup, Guid>()
                .ConvertUsing(src => src.GroupId);
        }
    }
}
