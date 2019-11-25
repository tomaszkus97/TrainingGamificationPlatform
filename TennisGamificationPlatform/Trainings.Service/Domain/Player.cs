using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trainings.Service.Domain
{
    public class Player
    {
        public Guid Id { get; set; }
        public virtual IEnumerable<PlayerTrainingGroup> PlayerTrainingGroups => _playerTrainingGroups;
        private readonly List<PlayerTrainingGroup> _playerTrainingGroups = new List<PlayerTrainingGroup>();
        public virtual IEnumerable<PlayerAttendance> PlayerAttendances => _playerAttendances;
        private readonly List<PlayerAttendance> _playerAttendances = new List<PlayerAttendance>();

        public Player() { }

        public Player(Guid id)
        {
            Id = id;
        }
    }
}
