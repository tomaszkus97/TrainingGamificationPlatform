using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Trainings.Service.Domain;
using Trainings.Service.Repositories;

namespace Trainings.Service.Events.External.Handlers
{
    public class CoachCreatedEventHandler : IEventHandler<CoachCreatedEvent>
    {
        private readonly TrainingsDbContext _dbContext;

        public CoachCreatedEventHandler(TrainingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task HandleAsync(CoachCreatedEvent @event)
        {
            var coach = new Coach(@event.CoachId, @event.Name, @event.Surname);
            await _dbContext.Coaches.AddAsync(coach);
            if (_dbContext.SaveChanges() == 0)
            {
                throw new Exception("Could not add new coach");
            }
        }
    }
}
