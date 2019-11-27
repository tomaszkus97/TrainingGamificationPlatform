using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trainings.Service.Dtos
{
    public class GroupAttendanceDto
    {
        public Guid GroupId { get; set; }
        public string Date { get; set; }
        public IEnumerable<Guid> AttendantPlayers { get; set; }
    }
}
