using System;
using System.Collections.Generic;
using Convey.CQRS.Queries;
using Trainings.Service.Dtos;

namespace Trainings.Service.Queries
{
    public class PlayerAttendanceQuery : IQuery<IEnumerable<PlayerAttendanceDto>>
    {
        public Guid PlayerId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}

