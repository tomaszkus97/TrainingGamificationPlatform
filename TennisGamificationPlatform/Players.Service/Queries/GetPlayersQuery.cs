using System;
using System.Collections.Generic;
using Convey.CQRS.Queries;
using Players.Service.Dtos;

namespace Players.Service.Queries
{
    public class GetPlayersQuery : IQuery<IEnumerable<PlayerDto>>
    {
        public IEnumerable<Guid> PlayerId { get; set; }
    }
}
