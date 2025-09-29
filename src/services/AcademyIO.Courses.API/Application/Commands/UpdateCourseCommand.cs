using AcademyIO.Core.Messages;
using FluentValidation;

namespace AcademyIO.Courses.API.Application.Commands
{
    public class UpdateCourseCommand : Command
    {
        public Guid CourseId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserCreationId { get; set; }
        public double Price { get; set; }

        public UpdateCourseCommand(string name, string description, Guid userCreationId, double price, Guid courseId)
        {

            this.Name = name;
            this.Description = description;
            this.UserCreationId = userCreationId;
            this.Price = price;
            this.CourseId = courseId;
        }


        public override bool IsValid()
        {
            ValidationResult = new UpdateCourseCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateCourseCommandValidation : AbstractValidator<UpdateCourseCommand>
    {
        public static string NameError => "O nome do curso não pode ser vazio.";
        public static string ContextError => "O conteúdo programático não pode ser vazio.";
        public static string UserCreationError => "O ID do usuário de criação não pode ser vazio.";
        public static string PriceErro => "O preço do curso deve ser maior que zero.";
        public static string IdError => "O ID do curso é obrigatório.";

        public UpdateCourseCommandValidation()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage(NameError);

            RuleFor(c => c.Description).NotEmpty().WithMessage(ContextError);

            RuleFor(c => c.UserCreationId).NotEmpty().WithMessage(UserCreationError);

            RuleFor(c => c.Price)
                .GreaterThan(0)
                .WithMessage(PriceErro);

            RuleFor(c => c.CourseId)
                .NotEqual(Guid.Empty).WithMessage(IdError);
        }
    }
}
