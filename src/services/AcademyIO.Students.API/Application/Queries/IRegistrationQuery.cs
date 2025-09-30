using AcademyIO.Students.API.Application.Queries.ViewModels;

namespace AcademyIO.Students.API.Application.Queries
{
    public interface IRegistrationQuery
    {
        List<RegistrationViewModel> GetByStudent(Guid studentId);

        List<RegistrationViewModel> GetAllRegistrations();
    }
}
