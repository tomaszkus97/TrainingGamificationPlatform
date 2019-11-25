using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;

namespace Trainings.Service.Queries
{
    public class CoachScheduleQuery : IQuery
    {
        public Guid CoachId { get; set; }
    }
}
