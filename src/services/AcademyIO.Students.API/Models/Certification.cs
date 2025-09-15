using AcademyIO.Core.Models;
using AcademyIO.Core.DomainObjects;

namespace AcademyIO.ManagementStudents.Models
{
    public class Certification : Entity
    {
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public User? Student { get; private set; }
    }
}
