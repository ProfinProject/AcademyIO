using AcademyIO.Students.API.Application.Queries.ViewModels;
using AcademyIO.Students.API.Models;

namespace AcademyIO.Students.API.Application.Queries
{
    public class RegistrationQuery : IRegistrationQuery
    {
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationQuery(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public List<RegistrationViewModel> GetByStudent(Guid studentId)
        {
            var registrations = _registrationRepository.GetRegistrationByStudent(studentId);

            return registrations.Select(c => new RegistrationViewModel
            {
                Id = c.Id,
                CourseId = c.CourseId,
                StudentId = c.StudentId,
                RegistrationTime = c.RegistrationTime,
                Status = c.Status
            }).ToList();
        }

        public List<RegistrationViewModel> GetAllRegistrations()
        {
            var registrations = _registrationRepository.GetAllRegistrations();

            return registrations.Select(c => new RegistrationViewModel
            {
                Id = c.Id,
                CourseId = c.CourseId,
                StudentId = c.StudentId,
                RegistrationTime = c.RegistrationTime,
                Status = c.Status
            }).ToList();
        }
        
    }
}
