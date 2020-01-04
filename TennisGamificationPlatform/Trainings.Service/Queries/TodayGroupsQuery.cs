using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Trainings.Service.Dtos;

namespace Trainings.Service.Queries
{
    public class TodayGroupsQuery : IQuery<IEnumerable<ScheduledTrainingDto>>
    {
        public Guid CoachId { get; set; }
    }
}
