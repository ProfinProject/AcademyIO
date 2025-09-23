using FluentValidation;
using AcademyIO.Core.Messages;

namespace AcademyIO.Students.API.Application.Commands;

public class AddRegistrationCommand(Guid studentId, Guid courseId) : Command
{
    public Guid StudentId { get; set; } = studentId;
    public Guid CourseId { get; set; } = courseId;

    public override bool IsValid()
    {
        ValidationResult = new AddRegistrationCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
public class AddRegistrationCommandValidation : AbstractValidator<AddRegistrationCommand>
{
    public static string StudentIdError = "O campo StudentId não pode ser vazio.";
    public static string CourseIdError = "O campo CourseId não pode ser vazio.";
    public AddRegistrationCommandValidation()
    {
        RuleFor(c => c.StudentId)
            .NotEqual(Guid.Empty)
            .WithMessage(StudentIdError);
        RuleFor(c => c.CourseId)
            .NotEqual(Guid.Empty)
            .WithMessage(CourseIdError);
    }
}