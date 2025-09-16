using AcademyIO.Core.Data;
using AcademyIO.Core.Models;

namespace AcademyIO.ManagementStudents.Models
{
    public interface IRegistrationRepository : IRepository<User>
    {
        Task<Registration> FinishCourse(Guid studentId, Guid courseId);
        Registration AddRegistration(Guid studentId, Guid courseId);
    }
}
