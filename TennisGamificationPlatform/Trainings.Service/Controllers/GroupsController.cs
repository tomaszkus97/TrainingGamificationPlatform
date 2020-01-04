using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using Trainings.Service.Commands;
using Trainings.Service.Dtos;
using Trainings.Service.Queries;

namespace Trainings.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public GroupsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult> GetGroups([FromQuery] IEnumerable<Guid> ids)
        {
            var query = new GetGroupsByIdQuery()
            {
                GroupIds = ids
            };
            var result = await _queryDispatcher.QueryAsync<IEnumerable<ScheduledTrainingDto>>(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("today-groups")]
        public async Task<ActionResult> GetTodayGroups([FromQuery] Guid coachId)
        {
            var query = new TodayGroupsQuery()
            {
                CoachId = coachId
            };
            var result = await _queryDispatcher.QueryAsync<IEnumerable<ScheduledTrainingDto>>(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("Coaches")]
        public async Task<ActionResult> GetCoaches()
        {
            var query = new GetCoachesQuery();
            var result = await _queryDispatcher.QueryAsync<IEnumerable<CoachDto>>(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateGroup(CreateGroupCommand command)
        {
            await _commandDispatcher.SendAsync<CreateGroupCommand>(command);
            return Ok();
        }

        [HttpPut("assign-player")]
        public async Task<ActionResult> AssignPlayer(AssignToGroupCommand command)
        {
            await _commandDispatcher.SendAsync<AssignToGroupCommand>(command);
            return Ok();
        }

        [HttpPut("assign-coach")]
        public async Task<ActionResult> AssignCoach(AssignCoachCommand command)
        {
            await _commandDispatcher.SendAsync<AssignCoachCommand>(command);
            return Ok();
        }

        [HttpDelete("remove-player")]
        public async Task<ActionResult> RemovePlayer(RemoveFromGroupCommand command)
        {
            await _commandDispatcher.SendAsync<RemoveFromGroupCommand>(command);
            return Ok();
        }
    }
}
