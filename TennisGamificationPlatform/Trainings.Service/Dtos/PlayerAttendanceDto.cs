using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trainings.Service.Dtos
{
    public class PlayerAttendanceDto
    {
        public Guid PlayerId { get; set; }
        public string Date { get; set; }
        public Guid GroupId { get; set; }
    }
}
