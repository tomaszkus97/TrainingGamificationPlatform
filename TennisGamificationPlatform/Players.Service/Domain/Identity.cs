using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Players.Service.Domain
{
    public class Identity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public Identity(Guid id, Guid userId, string name, string surname, int age)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Surname = surname;
            Age = age;
        }
    }
}
