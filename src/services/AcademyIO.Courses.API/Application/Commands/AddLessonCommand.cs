using AcademyIO.Core.Messages;
using FluentValidation;

namespace AcademyIO.Courses.API.Application.Commands
{
    public class AddLessonCommand : Command
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public Guid CourseId { get; set; }
        public double TotalHours { get; set; }

        public AddLessonCommand(string name, string subject, Guid courseId, double totalHours)
        {
            this.Name = name;
            this.Subject = subject;
            this.CourseId = courseId;
            this.TotalHours = totalHours;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddLessonCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddLessonCommandValidation : AbstractValidator<AddLessonCommand>
    {
        public static string NameError => "O nome da aula não pode ser vazio.";
        public static string SubjectError => "A área do conhecimento não pode ser vazia.";
        public static string UserCreationError => "O ID do usuário de criação não pode ser vazio.";
        public static string PriceErro => "O preço do curso deve ser maior que zero.";

        public AddLessonCommandValidation()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage(NameError);

            RuleFor(c => c.Subject).NotEmpty().WithMessage(SubjectError);

            RuleFor(c => c.CourseId).NotEmpty().WithMessage(UserCreationError);

            RuleFor(c => c.TotalHours)
                .GreaterThan(0)
                .WithMessage(PriceErro);
        }
    }
}
