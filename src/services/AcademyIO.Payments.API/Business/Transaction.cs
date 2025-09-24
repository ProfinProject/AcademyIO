using AcademyIO.Core.DomainObjects;

namespace AcademyIO.Payments.API.Business;

public class Transaction : Entity
{
    public Guid RegistrationId { get; set; }
    public Guid PaymentId { get; set; }
    public double Total { get; set; }
    public StatusTransaction StatusTransaction { get; set; }
    public Payment Payment { get; set; }
}