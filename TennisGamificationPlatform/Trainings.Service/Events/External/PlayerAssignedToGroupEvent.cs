using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Trainings.Service.Events.External
{
    [Message("players")]
    public class PlayerAssignedToGroupEvent : IEvent
    {
        public Guid PlayerId { get; set; }
        public Guid GroupId { get; set; }
    }
}
