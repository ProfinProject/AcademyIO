using AcademyIO.Core.Messages.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyIO.Core.Interfaces.Integration
{
    public class UserRegisteredIntegrationEvent : IntegrationEvent
    {
        public string Email { get; private set; }
        public bool IsAdmin { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }

        public UserRegisteredIntegrationEvent(string email, bool isAdmin, string firstName, string lastName, DateTime dateOfBirth)
        {
            this.Email = email;
            this.IsAdmin = isAdmin;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
        }
    }
}
