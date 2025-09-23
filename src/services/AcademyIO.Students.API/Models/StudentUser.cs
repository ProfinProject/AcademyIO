using AcademyIO.Core.DomainObjects;

namespace AcademyIO.Students.API.Models
{
    public class StudentUser : Entity, IAggregateRoot
    {
        public StudentUser(Guid id, string userName, string firstName, string lastName, string email, DateTime dateOfBirth, bool isAdmin)
          : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            UserName = userName;
            IsAdmin = isAdmin;
        }

        public string UserName { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = string.Empty;

        private readonly List<Registration> registrations = [];
        public IReadOnlyCollection<Registration> Registrations => registrations;
    }
}
