using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Players.Service.Domain;

namespace Players.Service.Dtos
{
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int Points { get; set; }
        public int LevelId { get; set; }
        public string CurrentLevelName { get; set; }
        public IEnumerable<Guid> AssignedGroups { get; set; }
    }
}
