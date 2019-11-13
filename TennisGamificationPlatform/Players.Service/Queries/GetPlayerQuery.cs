using System;
using Convey.CQRS.Queries;
using Players.Service.Dtos;

namespace Players.Service.Queries
{
    public class GetPlayerQuery : IQuery<PlayerDto>
    {
        public Guid PlayerId { get; set; }
    }
}
