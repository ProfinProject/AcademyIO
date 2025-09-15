using AcademyIO.Courses.API.Application.Queries.ViewModels;
using AcademyIO.Courses.API.Models;

namespace AcademyIO.Courses.API.Application.Queries
{
    public class CourseQuery : ICourseQuery
    {
        private readonly ICourseRepository _courseRepository;

        public CourseQuery(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseViewModel>> GetAll()
        {
            var courses = await _courseRepository.GetAll();

            return courses.Select(c => new CourseViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Price = c.Price
            }).ToList();
        }

        public async Task<CourseViewModel> GetById(Guid courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
                return null;

            var course = await _courseRepository.GetById(courseId);

            return new CourseViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
            };
        }
    }
}
