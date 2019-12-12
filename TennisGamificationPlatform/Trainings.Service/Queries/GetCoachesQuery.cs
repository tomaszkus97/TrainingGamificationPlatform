using System.Collections.Generic;
using Convey.CQRS.Queries;
using Trainings.Service.Dtos;

namespace Trainings.Service.Queries
{
    public class GetCoachesQuery : IQuery<IEnumerable<CoachDto>>
    {

    }
}
