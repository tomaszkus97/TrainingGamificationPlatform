using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Trainings.Service.Events.External
{
    [Message("identity")]
    public class CoachCreatedEvent : IEvent
    {
        public Guid CoachId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
