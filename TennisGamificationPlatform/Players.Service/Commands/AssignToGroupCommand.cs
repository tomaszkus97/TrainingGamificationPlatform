using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Newtonsoft.Json;

namespace Players.Service.Commands
{
    public class AssignToGroupCommand : ICommand
    {
        public Guid PlayerId { get; set; }
        public Guid GroupId { get; set; }

        public AssignToGroupCommand(Guid playerId, Guid groupId)
        {
            PlayerId = playerId;
            GroupId = groupId;
        }

        private AssignToGroupCommand() { }
     }
}
