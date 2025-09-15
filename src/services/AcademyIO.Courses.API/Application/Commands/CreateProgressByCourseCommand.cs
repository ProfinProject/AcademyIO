using AcademyIO.Core.Messages;
using FluentValidation;

namespace AcademyIO.Courses.API.Application.Commands
{
    public class CreateProgressByCourseCommand : Command
    {
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }

        public CreateProgressByCourseCommand(Guid courseId, Guid studentId)
        {
            this.CourseId = courseId;
            this.StudentId = studentId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateProgressByCourseCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CreateProgressByCourseCommandValidation : AbstractValidator<CreateProgressByCourseCommand>
    {
        public static string CourseIdError => "O ID do curso não pode ser vazio.";
        public static string StudentIdError => "O ID do aluno não pode ser vazio.";
        public CreateProgressByCourseCommandValidation()
        {
            RuleFor(c => c.CourseId).NotEmpty().WithMessage(CourseIdError);

            RuleFor(c => c.StudentId).NotEmpty().WithMessage(StudentIdError);
        }
    }
}
