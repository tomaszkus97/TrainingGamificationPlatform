using System;
using System.Collections.Generic;
using System.Linq;

namespace Trainings.Service.Domain
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public DateTime Date { get; set; }
        public virtual TrainingGroup Group { get; set; }
        public virtual IEnumerable<PlayerAttendance> AttendantPlayers => _attendantPlayers;
        private readonly List<PlayerAttendance> _attendantPlayers = new List<PlayerAttendance>();

        public Attendance()
        {

        }

        public Attendance(DateTime date, Guid groupId, IEnumerable<Player> players)
        {
            Id = Guid.NewGuid();
            GroupId = groupId;
            Date = date;
            var playerAttendances = players.Select(p => new PlayerAttendance()
            {
                AttendanceId = Id,
                PlayerId = p.Id
            });
            _attendantPlayers.AddRange(playerAttendances);
        }
    }
}