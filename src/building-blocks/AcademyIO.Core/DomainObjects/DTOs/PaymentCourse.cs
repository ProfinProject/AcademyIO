namespace AcademyIO.Core.DomainObjects.DTOs
{
    public class PaymentCourse
    {
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public double Total { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string CardCVV { get; set; }
    }
}
