using AcademyIO.Core.Messages;
using FluentValidation;

namespace AcademyIO.Courses.API.Application.Commands
{
    public class ValidatePaymentCourseCommand(Guid courseId, Guid studentId, string cardName, 
        string cardNumber, string cardExpirationDate, string cardCVV) : Command
    {
        public Guid CourseId { get; set; } = courseId;
        public Guid StudentId { get; set; } = studentId;
        public string CardName { get; set; } = cardName;
        public string CardNumber { get; set; } = cardNumber;
        public string CardExpirationDate { get; set; } = cardExpirationDate;
        public string CardCVV { get; set; } = cardCVV;
        public override bool IsValid()
        {
            ValidationResult = new ValidatePaymentCourseCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ValidatePaymentCourseCommandValidation : AbstractValidator<ValidatePaymentCourseCommand>
    {
        public static string CourseIdError = "O campo Course é obrigatório.";
        public static string StudentIdError = "O campo Student é obrigatório.";
        public static string CardNameError = "O campo Nome do Cartão é obrigatório.";
        public static string CardNumberError = "O campo Número do Cartão é obrigatório.";
        public static string CardExpirationDateError = "O campo Expiração do Cartão é obrigatório.";
        public static string CardCVVError = "O campo CVV do Cartão é obrigatório.";
        public static string InvalidCard = "O campo Número do Cartão inválido.";
        
        public ValidatePaymentCourseCommandValidation()
        {
            RuleFor(c => c.CourseId)
                .NotEqual(Guid.Empty)
                .WithMessage(CourseIdError);
            RuleFor(c => c.StudentId)
                .NotEqual(Guid.Empty)
                .WithMessage(StudentIdError);
            RuleFor(c => c.CardName)
                .NotEmpty()
                .WithMessage(CardNameError);
            RuleFor(c => c.CardNumber)
                .NotEmpty()
                .WithMessage(CardNumberError)
                .CreditCard()
                .WithMessage(InvalidCard);
            RuleFor(c => c.CardExpirationDate)
                .NotEmpty()
                .WithMessage(CardExpirationDateError);
            RuleFor(c => c.CardCVV)
                .NotEmpty()
                .WithMessage(CardCVVError);
        }
    }
}
