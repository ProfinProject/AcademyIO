using AcademyIO.Core.Messages;
using FluentValidation;

namespace AcademyIO.Courses.API.Application.Commands
{
    public class FinishLessonCommand : Command
    {
        public Guid LessonId { get; set; }
        public Guid StudentId { get; set; }

        public FinishLessonCommand(Guid lessonId, Guid studentId)
        {
            this.LessonId = lessonId;
            this.StudentId = studentId;
        }

        public override bool IsValid()
        {
            ValidationResult = new FinishLessonCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class FinishLessonCommandValidation : AbstractValidator<FinishLessonCommand>
    {
        public static string LessonIdError => "O ID da aula não pode ser vazio.";
        public static string StudentIdError => "O ID do usuário de criação não pode ser vazio..";

        public FinishLessonCommandValidation()
        {
            RuleFor(c => c.LessonId).NotEmpty().WithMessage(LessonIdError);

            RuleFor(c => c.StudentId).NotEmpty().WithMessage(StudentIdError);
        }
    }
}
