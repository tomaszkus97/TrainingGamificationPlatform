using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace Trainings.Service.Commands
{
    public class CreateGroupCommand : ICommand
    {
        public string Day { get; set; }
        public string Hour { get; set; }
        public string LevelName { get; set; }
        public Guid CoachId { get; set; }
    }
}
