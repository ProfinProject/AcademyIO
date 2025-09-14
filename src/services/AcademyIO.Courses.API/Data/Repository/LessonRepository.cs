using AcademyIO.Core.Data;
using AcademyIO.Core.Enums;
using AcademyIO.Courses.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademyIO.Courses.API.Data.Repository
{
    public class LessonRepository(CoursesContext _courseContext) : ILessonRepository
    {
        private readonly DbSet<Lesson> _dbSet = _courseContext.Set<Lesson>();
        public IUnitOfWork UnitOfWork => _courseContext;
        public void Add(Lesson lesson)
        {
            _dbSet.Add(lesson);
        }

        public Task Delete(Lesson entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }

        public async Task<IEnumerable<Lesson>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Lesson>> GetByCourseId(Guid courseId)
        {
            return await _dbSet.Where(a => a.CourseId == courseId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> CreateProgressLessonByCourse(Guid courseId, Guid studentId)
        {
            var lessons = await GetByCourseId(courseId);

            foreach (var lesson in lessons)
            {
                _courseContext.Add(new ProgressLesson(lesson.Id, studentId, EProgressLesson.NotStarted));
            }

            await _courseContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> StartLesson(Guid lessonId, Guid studentId)
        {
            if (!ExistProgress(lessonId, studentId))
                return false;

            ProgressLesson progressLesson = await _courseContext.ProgressLessons.FirstOrDefaultAsync(a => a.LessonId == lessonId && a.StudentId == studentId);
            progressLesson.ProgressionStatus = EProgressLesson.InProgress;
            _courseContext.ProgressLessons.Update(progressLesson);

            return true;
        }

        public async Task<bool> FinishLesson(Guid lessonId, Guid studentId)
        {
            if (!ExistProgress(lessonId, studentId))
                return false;

            ProgressLesson progressLesson = await _courseContext.ProgressLessons.FirstOrDefaultAsync(a => a.LessonId == lessonId && a.StudentId == studentId);
            progressLesson.ProgressionStatus = EProgressLesson.Completed;
            _courseContext.ProgressLessons.Update(progressLesson);

            return true;
        }
        public bool ExistProgress(Guid lessonId, Guid studentId)
        {
            return _courseContext.ProgressLessons.AsNoTracking().Any(a => a.LessonId == lessonId && a.StudentId == studentId);
        }

        public EProgressLesson GetProgressStatusLesson(Guid lessonId, Guid studentId)
        {
            return _courseContext.ProgressLessons.FirstOrDefault(a => a.LessonId == lessonId && a.StudentId == studentId).ProgressionStatus;
        }

        public async Task<IEnumerable<ProgressLesson>> GetProgression(Guid studentId)
        {
            return await _courseContext.ProgressLessons.Where(a => a.StudentId == studentId).Include(a => a.Lesson).ToListAsync();
        }
    }
}
