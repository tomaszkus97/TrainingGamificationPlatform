using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace Gamification.Service.Commands
{
    public class CreateChallengeCommand : ICommand
    {
        public int LevelId { get; set; }
        public bool IsObligatory { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
