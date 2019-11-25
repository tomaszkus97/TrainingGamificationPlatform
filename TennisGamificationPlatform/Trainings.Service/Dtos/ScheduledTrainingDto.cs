using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trainings.Service.Dtos
{
    public class ScheduledTrainingDto
    {
        public string GroupName { get; set; }
        public string CoachName { get; set; }
        public string LevelName { get; set; }
        public IEnumerable<PlayerDto> Players { get; set; }
        public ChallengeDto OptionalChallenge { get; set; }

    }
}
