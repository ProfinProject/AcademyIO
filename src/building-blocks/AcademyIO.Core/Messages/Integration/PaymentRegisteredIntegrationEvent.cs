using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyIO.Core.Messages.Integration
{
    public class PaymentRegisteredIntegrationEvent(Guid courseId, Guid studentId, string cardName,
        string cardNumber, string cardExpirationDate, string cardCVV) : IntegrationEvent
    {
        public Guid CourseId { get; set; } = courseId;
        public Guid StudentId { get; set; } = studentId;
        public string CardName { get; set; } = cardName;
        public string CardNumber { get; set; } = cardNumber;
        public string CardExpirationDate { get; set; } = cardExpirationDate;
        public string CardCVV { get; set; } = cardCVV;
    }
}
