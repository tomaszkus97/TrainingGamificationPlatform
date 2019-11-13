using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Service.Domain
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public Role Role { get; private set; }

        public User(string username, string password, string role)
        {
            Role parsedRole;
            if (string.IsNullOrEmpty(username))
            {
                throw new Exception();
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception();
            }
            if (!Enum.TryParse<Role>(role,out parsedRole))
            {
                throw new Exception();
            }

            Username = username;
            Password = password;
            Role = parsedRole;
        }

        public User() { }
    }
}
