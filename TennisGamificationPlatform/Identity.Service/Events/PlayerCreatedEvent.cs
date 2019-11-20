using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Identity.Service.Events
{
    [Message("identity")]
    public class PlayerCreatedEvent : IEvent
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
    }
}
