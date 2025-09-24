using AcademyIO.Core.Data;

namespace AcademyIO.Payments.API.Business;

public interface IPaymentRepository : IRepository<Payment>
{
    void Add(Payment payment);
    void AddTransaction(Transaction transaction);

    Task<bool> PaymentExists(Guid studentId, Guid courseId);
}