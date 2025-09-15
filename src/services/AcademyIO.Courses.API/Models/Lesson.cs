using AcademyIO.Core.DomainObjects;

namespace AcademyIO.Courses.API.Models
{
    public class Lesson(string name, string subject, double totalHours, Guid courseId) : Entity, IAggregateRoot
    {
        public string Name { get; set; } = name;
        public string Subject { get; set; } = subject;
        public double TotalHours { get; set; } = totalHours;
        public Guid CourseId { get; set; } = courseId;
    }
}
