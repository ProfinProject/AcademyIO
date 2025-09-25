using AcademyIO.Core.Messages;
using FluentValidation;

namespace AcademyIO.Students.API.Application.Commands
{
    public class AddUserCommand : Command
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = string.Empty;

        public AddUserCommand(Guid userId, string userName, bool isAdmin, string name, string lastName, DateTime dateOfBirth, string email)
        {
            UserId = userId;
            UserName = userName;
            IsAdmin = isAdmin;
            Name = name;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddUserCommandValidation : AbstractValidator<AddUserCommand>
    {
        public static string UserNameError = "O campo StudentId não pode ser vazio.";
        public static string NameError = "O campo CourseId não pode ser vazio.";
        public static string LastNameError = "O campo CourseId não pode ser vazio.";
        public static string EmailError = "O campo CourseId não pode ser vazio.";

        public AddUserCommandValidation()
        {
            RuleFor(c => c.UserName)
                .NotEmpty()
                .WithMessage(UserNameError);

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage(NameError);

            RuleFor(c => c.LastName)
                .NotEmpty()
                .WithMessage(LastNameError);

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage(EmailError);
        }
    }
}
