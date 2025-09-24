using AcademyIO.Payments.API.Business;

namespace AcademyIO.Payments.API.AntiCorruption;

public interface IPayPalGateway
{
    string GetPayPalServiceKey(string apiKey, string encriptionKey);
    string GetCardHashKey(string serviceKey, string cartaoCredito);
    Transaction CommitTransaction(string cardHashKey, string orderId, double amount);
}