using AcademyIO.Core.Messages;
using FluentValidation;

namespace AcademyIO.Courses.API.Application.Commands
{
    public class RemoveCourseCommand : Command
    {
        public Guid CourseId { get; private set; }        

        public RemoveCourseCommand(Guid courseId)
        {            
            this.CourseId = courseId;
        }


        public override bool IsValid()
        {
            ValidationResult = new RemoveCourseCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoveCourseCommandValidation : AbstractValidator<RemoveCourseCommand>
    {
        public static string IdError => "O ID do curso é obrigatório.";

        public RemoveCourseCommandValidation()
        {
            RuleFor(c => c.CourseId)
                .NotEqual(Guid.Empty).WithMessage(IdError);
        }
    }
}
