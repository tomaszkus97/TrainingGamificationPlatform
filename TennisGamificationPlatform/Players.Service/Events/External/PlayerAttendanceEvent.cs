using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Players.Service.Events.External
{
    [Message("trainings")]
    public class PlayerAttendanceEvent : IEvent
    {
        public Guid PlayerId { get; set; }
    }
}
