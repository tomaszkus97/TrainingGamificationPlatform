using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Gamification.Service.Commands;
using Gamification.Service.Dtos;
using Gamification.Service.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Gamification.Service.Controllers
{
    [Route("api/[controller]")]
    public class ChallengesController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public ChallengesController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChallengeDto>>> GetCHallenges([FromQuery] IEnumerable<Guid> Id)
        {
            var query = new GetChallengesQuery()
            {
                ChallengeId = Id
            };
            var result = await _queryDispatcher.QueryAsync<IEnumerable<ChallengeDto>>(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateChallenge(CreateChallengeCommand command)
        {
            await _commandDispatcher.SendAsync<CreateChallengeCommand>(command);
            return Ok();
        }
    }
}
