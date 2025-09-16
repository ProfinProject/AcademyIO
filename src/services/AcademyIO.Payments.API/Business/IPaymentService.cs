using AcademyIO.Core.DomainObjects.DTOs;

namespace AcademyIO.Payments.API.Business;

public interface IPaymentService
{
    Task<bool> MakePaymentCourse(PaymentCourse paymentCourse);
}