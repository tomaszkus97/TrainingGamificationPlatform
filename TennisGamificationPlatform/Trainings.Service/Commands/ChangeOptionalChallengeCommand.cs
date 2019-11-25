using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace Trainings.Service.Commands
{
    public class ChangeOptionalChallengeCommand : ICommand
    {
        public Guid GroupId { get; set; }
        public Guid ChallengeId { get; set; }
    }
}
