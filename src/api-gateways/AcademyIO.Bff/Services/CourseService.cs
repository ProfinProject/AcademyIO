using AcademyIO.Bff.Models;

namespace AcademyIO.Bff.Services
{
    public interface ICourseService
    {
        Task<CourseViewModel> GetById(Guid id);
    }

    public class CourseService : ICourseService
    {
        public Task<CourseViewModel> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
