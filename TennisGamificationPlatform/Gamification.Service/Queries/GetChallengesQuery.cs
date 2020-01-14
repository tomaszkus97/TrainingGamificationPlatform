using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Gamification.Service.Dtos;

namespace Gamification.Service.Queries
{
    public class GetChallengesQuery : IQuery<IEnumerable<ChallengeDto>>
    {
        public IEnumerable<Guid> ChallengeId { get; set; }
    }
}
