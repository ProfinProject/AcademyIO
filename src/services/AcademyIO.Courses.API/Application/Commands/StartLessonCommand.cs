using AcademyIO.Core.Messages;
using FluentValidation;

namespace AcademyIO.Courses.API.Application.Commands
{
    public class StartLessonCommand : Command
    {
        public Guid LessonId { get; set; }
        public Guid StudentId { get; set; }

        public StartLessonCommand(Guid lessonId, Guid studentId)
        {
            this.LessonId = lessonId;
            this.StudentId = studentId;
        }

        public override bool IsValid()
        {
            ValidationResult = new StartLessonCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class StartLessonCommandValidation : AbstractValidator<StartLessonCommand>
    {
        public static string LessonIdError => "O ID da aula não pode ser vazio.";
        public static string StudentIdError => "O ID do usuário de criação não pode ser vazio..";

        public StartLessonCommandValidation()
        {
            RuleFor(c => c.LessonId).NotEmpty().WithMessage(LessonIdError);

            RuleFor(c => c.StudentId).NotEmpty().WithMessage(StudentIdError);
        }
    }
}
