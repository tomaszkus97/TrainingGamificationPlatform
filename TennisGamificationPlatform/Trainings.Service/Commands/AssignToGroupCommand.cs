using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace Trainings.Service.Commands
{
    public class AssignToGroupCommand : ICommand
    {
        public Guid PlayerId { get; set; }
        public Guid GroupId { get; set; }
    }
}
