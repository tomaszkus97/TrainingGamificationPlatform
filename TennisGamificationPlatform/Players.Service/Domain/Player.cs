using System;
using System.Collections.Generic;
using System.Linq;
using Players.Service.Contracts;

namespace Players.Service.Domain
{
    public class Player
    {
        public Guid Id { get; set; }
        public Guid IdentityId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int Points { get; set; }
        public int LevelId { get; set; }
        public virtual Level CurrentLevel { get; set; }
        public virtual IEnumerable<PlayerGroup> AssignedGroups => _assignedGroups;
        public virtual IEnumerable<Challenge> CompletedChallenges => _completedChallenges;
        private readonly List<PlayerGroup> _assignedGroups = new List<PlayerGroup>();
        private readonly List<Challenge> _completedChallenges = new List<Challenge>();

        public Player()
        {

        }

        public Player(Guid id, Guid identityId, string name, string surname, int age)
        {
            Id = id;
            IdentityId = identityId;
            Name = name;
            Surname = surname;
            Age = age;
            Points = 0;
            LevelId = 1;
        }

        public void ChallengeCompleted(Guid challengeId, int pointsGranted)
        {
            Points += pointsGranted;
            var challenge = CompletedChallenges.FirstOrDefault(c => c.Id == challengeId);
            if (challenge == null)
            {
                var newChallenge = new Challenge() { Id = challengeId };
                _completedChallenges.Add(newChallenge);
            }
            else
            {
                throw new Exception("Challenge was already completed");
            }
        }

        public void SetLevel(int levelId, LevelName name, int pointsToAdvance)
        {
            LevelId = levelId;
            CurrentLevel = new Level(levelId, name, pointsToAdvance);
        }

        public bool DoAdvanced() =>
             Points >= CurrentLevel.PointsToAdvance ? true : false;

        public void AssignToGroup(Guid groupId)
        {
            var newGroup = new PlayerGroup()
            {
                Id = new Guid(),
                GroupId = groupId ,
                PlayerId = Id
            };
            _assignedGroups.Add(newGroup);
        }

        public void AwardAttendancePoints()
        {
            Points += 2;
        }

    }
}
