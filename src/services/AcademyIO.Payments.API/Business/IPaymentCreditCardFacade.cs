namespace AcademyIO.Payments.API.Business;

public interface IPaymentCreditCardFacade
{
    BusinessTransaction MakePayment(Payment payment);
}