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
    public class AttendanceController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public AttendanceController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<ActionResult> FillAttendance(FillAttendanceCommand command)
        {
            await _commandDispatcher.SendAsync<FillAttendanceCommand>(command);
            return Ok();
        }

        [HttpGet("player/{playerId}")]
        public async Task<ActionResult> GetPlayerAttendance(Guid playerId, [FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            var query = new PlayerAttendanceQuery()
            {
                PlayerId = playerId,
                DateFrom = dateFrom,
                DateTo = dateTo
            };
            var results = await _queryDispatcher.QueryAsync<IEnumerable<PlayerAttendanceDto>>(query);
            if (results == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(results);
            }
        }

        [HttpGet("group/{groupId}")]
        public async Task<ActionResult> GetGroupAttendance(Guid groupId, [FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            var query = new GroupAttendanceQuery()
            {
                GroupId = groupId,
                DateFrom = dateFrom,
                DateTo = dateTo
            };
            var results = await _queryDispatcher.QueryAsync<IEnumerable<GroupAttendanceDto>>(query);
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
