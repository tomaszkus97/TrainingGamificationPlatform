using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using Players.Service.Commands;
using Players.Service.Dtos;
using Players.Service.Queries;

namespace Players.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public PlayersController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet("/{playerId}")]
        public async Task<ActionResult<PlayerDto>> GetPlayer([FromRoute] Guid playerId)
        {
            var query = new GetPlayerQuery() { PlayerId = playerId };
            var result = await _queryDispatcher.QueryAsync<PlayerDto>(query);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayers([FromQuery] IEnumerable<Guid> Id)
        {
            var query = new GetPlayersQuery()
            {
                PlayerId = Id
            };
            var result = await _queryDispatcher.QueryAsync<IEnumerable<PlayerDto>>(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("assign-group")]
        public async Task<ActionResult> AssignToGroup(AssignToGroupCommand command)
        {
            await _commandDispatcher.SendAsync<AssignToGroupCommand>(command);
            return Ok();
        }

        [HttpPut("change-level")]
        public async Task<ActionResult> ChangeLevel(ChangeLevelCommand command)
        {
            await _commandDispatcher.SendAsync<ChangeLevelCommand>(command);
            return Ok();
        }

    }
}