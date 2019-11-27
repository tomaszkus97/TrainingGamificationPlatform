using System;
using System.Collections.Generic;
using Convey.CQRS.Queries;
using Trainings.Service.Dtos;

namespace Trainings.Service.Queries
{
    public class GroupAttendanceQuery : IQuery<IEnumerable<GroupAttendanceDto>>
    {
        public Guid GroupId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}
