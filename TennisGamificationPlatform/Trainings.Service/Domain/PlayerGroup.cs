using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trainings.Service.Domain
{
    public class PlayerTrainingGroup
    {
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public Guid TrainingGroupId { get; set; }
        public virtual TrainingGroup TrainingGroup { get; set; }
    }
}
