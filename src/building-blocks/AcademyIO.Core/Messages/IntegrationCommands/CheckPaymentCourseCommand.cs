using FluentValidation;

namespace AcademyIO.Core.Messages.IntegrationCommands;

public class CheckPaymentCourseCommand(Guid studentId, Guid courseId) : Command
{
    public Guid StundentId { get; set; } = studentId;
    public Guid CourseId { get; set; } = courseId;
    public override bool IsValid()
    {
        ValidationResult = new CheckPaymentCourseCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
public class CheckPaymentCourseCommandValidation : AbstractValidator<CheckPaymentCourseCommand>
{
    public static string StudentIdError = "O campo AlunoId não pode ser vazio.";
    public static string CourseIdError = "O campo CursoId não pode ser vazio.";
    public CheckPaymentCourseCommandValidation()
    {
        RuleFor(c => c.StundentId)
            .NotEqual(Guid.Empty)
            .WithMessage(StudentIdError);
        RuleFor(c => c.CourseId)
            .NotEqual(Guid.Empty)
            .WithMessage(CourseIdError);
    }
}