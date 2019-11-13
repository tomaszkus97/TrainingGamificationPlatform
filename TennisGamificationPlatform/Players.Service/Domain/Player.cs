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
        public Level CurrentLevel { get; set; }
        public IEnumerable<Guid> AssignedGroups => _groups.AsReadOnly();
        public IEnumerable<Guid> CompletedChallenges => _completedChallenges.AsReadOnly();
        private readonly List<Guid> _groups = new List<Guid>();
        private readonly List<Guid> _completedChallenges = new List<Guid>();

        private Player()
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
            var challenge = CompletedChallenges.FirstOrDefault(c => c == challengeId);
            if (challenge == null)
            {
                _completedChallenges.Add(challengeId);
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
            var group = AssignedGroups.FirstOrDefault(g => g == groupId);
            if (group == null)
            {
                _groups.Add(groupId);
            }
            else
            {
                throw new Exception("Group was already assigned");
            }
        }

    }
}
