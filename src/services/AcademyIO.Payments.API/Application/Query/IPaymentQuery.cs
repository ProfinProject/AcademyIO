namespace AcademyIO.Payments.API.Application.Query
{
    public interface IPaymentQuery
    {
        Task<bool> PaymentExists(Guid studentId, Guid courseId);
    }
}
