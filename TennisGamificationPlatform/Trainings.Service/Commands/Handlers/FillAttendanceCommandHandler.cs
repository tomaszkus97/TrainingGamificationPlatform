using System;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using Trainings.Service.Domain;
using Trainings.Service.Events;
using Trainings.Service.Repositories;

namespace Trainings.Service.Commands.Handlers
{
    public class FillAttendanceCommandHandler : ICommandHandler<FillAttendanceCommand>
    {
        private readonly TrainingsDbContext _dbContext;
        private readonly IBusPublisher _busPublisher;

        public FillAttendanceCommandHandler(TrainingsDbContext dbContext, IBusPublisher busPublisher)
        {
            _dbContext = dbContext;
            _busPublisher = busPublisher;
        }
        public async Task HandleAsync(FillAttendanceCommand command)
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
            foreach (var player in attendance.AttendantPlayers)
            {
                var @event = new PlayerAttendanceEvent()
                {
                    PlayerId = player.PlayerId
                };
                await _busPublisher.PublishAsync(@event);
            }
        }
    }
}
