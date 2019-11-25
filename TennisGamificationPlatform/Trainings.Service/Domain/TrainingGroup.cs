using System;
using System.Collections.Generic;
using System.Linq;

namespace Trainings.Service.Domain
{
    public class TrainingGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan Hour { get; set; }
        public Guid? CoachId { get; set; }
        public virtual Coach Coach { get; set; }
        public string LevelName { get; set; }
        public virtual IEnumerable<PlayerTrainingGroup> PlayerTrainingGroups => _playerTrainingGroups;
        public virtual IEnumerable<Attendance> Attendances => _attendances;
        public Guid CurrentOptionalChallenge { get; set; }

        private readonly List<PlayerTrainingGroup> _playerTrainingGroups = new List<PlayerTrainingGroup>();
        private readonly List<Attendance> _attendances = new List<Attendance>();

        public TrainingGroup(string day, string hour, string levelName)
        {
            Id = Guid.NewGuid();
            Day = Enum.Parse<DayOfWeek>(day);
            Hour = TimeSpan.Parse(hour);
            Name = $"{ day.ToString()} {hour}";
            LevelName = levelName;
        }

        public TrainingGroup() { }

        public void AssignPlayer(Guid playerId)
        {
            if(_playerTrainingGroups.FirstOrDefault(p=>p.PlayerId == playerId) == null)
            {
                _playerTrainingGroups.Add(new PlayerTrainingGroup() 
                { 
                    PlayerId = playerId,
                    TrainingGroupId = Id
                });
            }
            else
            {
                throw new Exception("Player is already assigned to that group");
            }
        }

        public void RemovePlayer(Guid playerId)
        {
            var player = _playerTrainingGroups.FirstOrDefault(p => p.PlayerId == playerId);
            if(player != null)
            {
                _playerTrainingGroups.Remove(player);
            }
            else
            {
                throw new Exception("Player is not assigned to that group");
            }
        }

        public void FillAttendance(params Player[] players)
        {
            var today = DateTime.Today;
            var attendance = new Attendance(today, players)
            {
                GroupId = Id
            };
            _attendances.Add(attendance);
        }

        public void SetCoach(Guid coachId)
        {
            if(CoachId != coachId)
            {
                CoachId = coachId;
            }
            else
            {
                throw new Exception("Requested coach is already set for that group");
            }
        }

        public void SetOptionalChallenge(Guid challengeId)
        {
            if (CurrentOptionalChallenge != challengeId)
            {
                CurrentOptionalChallenge = challengeId;
            }
            else
            {
                throw new Exception("Requested challenge is already assigned to that group");
            }
        }
    }
}
