using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainings.Service.Dtos;

namespace Trainings.Service.Services.Clients
{
    public interface IPlayersServiceClient
    {
        Task<IEnumerable<PlayerDto>> GetPlayers(IEnumerable<Guid> PlayerIds);
    }
}
