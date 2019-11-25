using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trainings.Service.Domain
{
    public class PlayerAttendance
    {
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public Guid AttendanceId { get; set; }
        public virtual Attendance Attendance { get; set; }
    }
}
