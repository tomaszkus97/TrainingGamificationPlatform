using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Players.Service.Contracts;

namespace Players.Service.Domain
{
    public class Level
    {
        public int LevelId { get; set; }
        public LevelName Name { get; set; }
        public int PointsToAdvance { get; set; }

        public Level(int levelId, LevelName name, int pointsToAdvance)
        {
            LevelId = levelId;
            Name = name;
            PointsToAdvance = pointsToAdvance;
        }

    }
}
