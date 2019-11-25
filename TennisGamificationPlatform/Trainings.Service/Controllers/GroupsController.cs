using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using Trainings.Service.Commands;

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
