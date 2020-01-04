using System;
using System.Collections.Generic;
using Convey.CQRS.Commands;

namespace Trainings.Service.Commands
{
    public class FillAttendanceCommand : ICommand
    {
        public Guid GroupId { get; set; }
        public string Date { get; set; }
        public IEnumerable<Guid> AttendantPlayers { get; set; }
    }
}
