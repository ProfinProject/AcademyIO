using AcademyIO.Core.Data;
using AcademyIO.Core.Enums;
using AcademyIO.ManagementStudents.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademyIO.ManagementStudents.Data.Repository
{
    public class RegistrationRepository(StudentsContext db) : IRegistrationRepository
    {
        private readonly DbSet<Registration> _dbSet = db.Set<Registration>();
        public IUnitOfWork UnitOfWork => db;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<Registration> FinishCourse(Guid studentId, Guid courseId)
        {
            //Valida que todas as lessons foram feitas
            var lesson = await _dbSet.FirstOrDefaultAsync(a => a.StudentId == studentId && a.CourseId == courseId);
            if (lesson != null)
                lesson.Status = EProgressLesson.Completed;
            return lesson;
        }

        public Registration AddRegistration(Guid studentId, Guid courseId)
        {
            if (RegistrationExists(studentId, courseId))
                throw new Exception("Matrícula já existente.");

            var registration = new Registration(studentId, courseId, DateTime.Now);


            _dbSet.Add(registration);

            return registration;
        }

        private bool RegistrationExists(Guid studentId, Guid courseId)
        {
            return _dbSet.Any(x => x.StudentId == studentId && x.CourseId == courseId);
        }
    }
}
