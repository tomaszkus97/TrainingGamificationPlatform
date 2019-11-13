using System;
using Convey.CQRS.Commands;
using Players.Service.Contracts;

namespace Players.Service.Commands
{
    public class ChangeLevelCommand : ICommand
    {
        public Guid PlayerId  { get; }
        public int LevelId { get; }
        public LevelName LevelName { get; }
        public int PointsToAdvance { get; }

        public ChangeLevelCommand(Guid playerId, int levelId, LevelName levelName, int pointsToAdvance)
        {
            PlayerId = playerId;
            LevelId = levelId;
            LevelName = levelName;
            PointsToAdvance = pointsToAdvance;
        }
    }
}
