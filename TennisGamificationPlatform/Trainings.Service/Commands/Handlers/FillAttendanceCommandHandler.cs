using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Trainings.Service.Domain;
using Trainings.Service.Repositories;

namespace Trainings.Service.Commands.Handlers
{
    public class FillAttendanceCommandHandler : ICommandHandler<FillAttendanceCommand>
    {
        private readonly TrainingsDbContext _dbContext;

        public FillAttendanceCommandHandler(TrainingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task HandleAsync(FillAttendanceCommand command)
        {
            var attendantPlayers = command.AttendantPlayers.Select(id => new Player()
            {
                Id = id
            });
            var date = DateTime.Parse(command.Date);
            var attendance = new Attendance(date, command.GroupId, attendantPlayers);
            _dbContext.Attendances.Add(attendance);
            if (_dbContext.SaveChanges() == 0)
            {
                throw new Exception("Could not add attendance");
            }

            return Task.CompletedTask;
        }
    }
}
