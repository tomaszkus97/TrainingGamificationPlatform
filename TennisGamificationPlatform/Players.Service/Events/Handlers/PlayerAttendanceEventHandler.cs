using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Players.Service.Events.External;
using Players.Service.Repositories;

namespace Players.Service.Events.Handlers
{
    public class PlayerAttendanceEventHandler : IEventHandler<PlayerAttendanceEvent>
    {
        private readonly PlayersDbContext _dbContext;

        public PlayerAttendanceEventHandler(PlayersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task HandleAsync(PlayerAttendanceEvent @event)
        {
            var player = _dbContext.Players.FirstOrDefault(p => p.Id == @event.PlayerId);
            if(player == null)
            {
                throw new Exception("Requested player does not exist");
            }
            else
            {
                player.AwardAttendancePoints();
                if (player.DoAdvanced())
                {
                    //Todo handle advance!
                }
            }
            return Task.CompletedTask;
        }
    }
}
