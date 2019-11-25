using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using Trainings.Service.Dtos;
using Trainings.Service.Queries;

namespace Trainings.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public ScheduleController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet("player/{playerId}")]
        public async Task<ActionResult> GetPlayerSchedule(Guid playerId)
        {
            var query = new PlayerScheduleQuery()
            {
                PlayerId = playerId
            };
            var results = await _queryDispatcher.QueryAsync<IEnumerable<ScheduledTrainingDto>>(query);
            if(results == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(results);
            }
        }

        [HttpGet("coach/{coachId}")]
        public async Task<ActionResult> GetCoachSchedule(Guid coachId)
        {
            var query = new CoachScheduleQuery()
            {
                CoachId = coachId
            };
            var results = await _queryDispatcher.QueryAsync<IEnumerable<ScheduledTrainingDto>>(query);
            if (results == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(results);
            }
        }
    }
}
