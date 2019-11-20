using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Events;

namespace Common.Contracts.Events
{
    public class PlayerCreatedEvent : IEvent
    {
        public Guid PlayerId{ get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age  { get; set; }
    }
}
