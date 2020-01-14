using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamification.Service.Domain
{
    public class Challenge
    {
        public Guid Id { get; set; }
        public int LevelId { get; set; }
        public bool IsObligatory { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
