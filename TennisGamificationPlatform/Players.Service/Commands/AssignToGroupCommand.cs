using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace Players.Service.Commands
{
    public class AssignToGroupCommand : ICommand
    {
        public Guid PlayerId { get; }
        public Guid GroupId { get; }

        public AssignToGroupCommand(Guid playerId, Guid groupId)
        {
            PlayerId = playerId;
            GroupId = groupId;
        }
     }
}
