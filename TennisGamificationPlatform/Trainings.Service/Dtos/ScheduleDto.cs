using System.Collections.Generic;

namespace Trainings.Service.Dtos
{
    public class ScheduleDto
    {
        public string Day { get; set; }
        public IEnumerable<ScheduledTrainingDto> Trainings { get; set; }
    }
}
