using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Players.Service.Events
{
    [Message("players")]
    public class PlayerAssignedToGroupEvent : IEvent
    {
        public Guid PlayerId { get; set; }
        public Guid GroupId { get; set; }
    }
}
