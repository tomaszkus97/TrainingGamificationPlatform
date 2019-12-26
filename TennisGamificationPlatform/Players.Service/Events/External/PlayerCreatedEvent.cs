using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Players.Service.Events.External
{
    [Message("identity")]
    public class PlayerCreatedEvent : IEvent
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string LevelName { get; set; }
    }
}
