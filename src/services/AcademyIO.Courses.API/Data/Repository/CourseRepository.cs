using AcademyIO.Core.Data;
using AcademyIO.Courses.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademyIO.Courses.API.Data.Repository
{
    public class CourseRepository(CoursesContext courseContext) : ICourseRepository
    {
        private readonly DbSet<Course> _dbSet = courseContext.Set<Course>();
        public IUnitOfWork UnitOfWork => courseContext;
        public void Add(Course course)
        {
            _dbSet.Add(course);
        }

        public void Dispose()
        {

        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<Course> GetById(Guid courseId)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Id == courseId);
        }

        public bool CourseExists(Guid courseId)
        {
            return _dbSet.Any(a => a.Id == courseId);
        }

        public void Update(Course course)
        {
            _dbSet.Update(course);
        }

        public void Delete(Course course)
        {
            _dbSet.Remove(course);
        }
    }
}
