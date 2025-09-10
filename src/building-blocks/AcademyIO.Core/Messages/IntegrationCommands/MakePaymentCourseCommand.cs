using FluentValidation;
using AcademyIO.Core.DomainObjects.DTOs;

namespace AcademyIO.Core.Messages.IntegrationCommands;

public class MakePaymentCourseCommand(PaymentCourse paymentCourse) : Command
{
    public PaymentCourse PaymentCourse { get; set; } = paymentCourse;
    public override bool IsValid()
    {
        ValidationResult = new MakePaymentCourseCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
public class MakePaymentCourseCommandValidation : AbstractValidator<MakePaymentCourseCommand>
{
    public static string PaymentCourseError => "O pagamento do curso não pode ser vazio.";
    public MakePaymentCourseCommandValidation()
    {
        RuleFor(c => c.PaymentCourse).NotEmpty().WithMessage(PaymentCourseError);
    }
}