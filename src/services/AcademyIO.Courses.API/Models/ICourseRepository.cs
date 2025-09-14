using AcademyIO.Core.Data;

namespace AcademyIO.Courses.API.Models
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetAll();

        Task<Course> GetById(Guid courseId);

        void Add(Course course);

        bool CourseExists(Guid courseI);
    }
}
