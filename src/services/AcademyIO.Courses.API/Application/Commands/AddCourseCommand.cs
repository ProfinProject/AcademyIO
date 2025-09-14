using AcademyIO.Core.Messages;
using FluentValidation;

namespace AcademyIO.Courses.API.Application.Commands
{
    public class AddCourseCommand : Command
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserCreationId { get; set; }
        public double Price { get; set; }

        public AddCourseCommand(string name, string description, Guid userCreationId, double price)
        {
            this.Name = name;
            this.Description = description;
            this.UserCreationId = userCreationId;
            this.Price = price;
        }


        public override bool IsValid()
        {
            ValidationResult = new AddCourseCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddCourseCommandValidation : AbstractValidator<AddCourseCommand>
    {
        public static string NameError => "O nome do curso não pode ser vazio.";
        public static string ContextError => "O conteúdo programático não pode ser vazio.";
        public static string UserCreationError => "O ID do usuário de criação não pode ser vazio.";
        public static string PriceErro => "O preço do curso deve ser maior que zero.";

        public AddCourseCommandValidation()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage(NameError);

            RuleFor(c => c.Description).NotEmpty().WithMessage(ContextError);

            RuleFor(c => c.UserCreationId).NotEmpty().WithMessage(UserCreationError);

            RuleFor(c => c.Price)
                .GreaterThan(0)
                .WithMessage(PriceErro);
        }
    }
}
