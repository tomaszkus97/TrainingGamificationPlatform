using System;

namespace Trainings.Service.Dtos
{
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LevelName { get; set; }
    }
}