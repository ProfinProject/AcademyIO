using AcademyIO.Core.Data;
using AcademyIO.Core.DomainObjects;

namespace AcademyIO.Students.API.Models
{
    public interface IRegistrationRepository : IRepository<User>
    {
        Task<Registration> FinishCourse(Guid studentId, Guid courseId);
        Registration AddRegistration(Guid studentId, Guid courseId);
        List<Registration> GetRegistrationByStudent(Guid studentId);
        List<Registration> GetAllRegistrations();
    }
}
