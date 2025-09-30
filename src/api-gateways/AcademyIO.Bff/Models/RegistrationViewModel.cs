using AcademyIO.Core.Enums;

namespace AcademyIO.Bff.Models
{
    public class RegistrationViewModel
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime RegistrationTime { get; set; }
        public EProgressLesson Status { get; set; }
    }
}
