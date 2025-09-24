using System;

namespace AcademyIO.Core.Messages.Integration
{
    public class UserRegisteredIntegrationEvent : IntegrationEvent
    {
        public UserRegisteredIntegrationEvent(Guid id, string firstName, string lastName, string userName, DateTime dateOfBirth, bool isAdmin)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            DateOfBirth = dateOfBirth;
            IsAdmin = isAdmin;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public bool IsAdmin { get; private set; }
    }
}