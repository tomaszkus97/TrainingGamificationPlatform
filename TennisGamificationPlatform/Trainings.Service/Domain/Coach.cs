using System;
using System.Collections.Generic;

namespace Trainings.Service.Domain
{
    public class Coach
    {
        public Guid Id { get; set; }
        public Guid IdentityId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual IEnumerable<TrainingGroup> Groups => _groups;
        private readonly List<TrainingGroup> _groups = new List<TrainingGroup>();

        public Coach() { }

        public Coach(Guid identityId, string name, string surname)
        {
            Id = identityId;
            Name = name;
            Surname = surname;
            IdentityId = Guid.NewGuid();
        }
    }
}