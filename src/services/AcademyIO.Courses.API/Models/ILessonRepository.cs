using AcademyIO.Core.Data;
using AcademyIO.Core.Enums;

namespace AcademyIO.Courses.API.Models
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Task<IEnumerable<Lesson>> GetAll();
        Task<IEnumerable<Lesson>> GetByCourseId(Guid courseId);
        Task<IEnumerable<ProgressLesson>> GetProgression(Guid studentId);
        void Add(Lesson course);
        Task<bool> CreateProgressLessonByCourse(Guid courseId, Guid studentId);
        Task<bool> StartLesson(Guid lessonId, Guid studentId);
        Task<bool> FinishLesson(Guid lessonId, Guid studentId);
        bool ExistProgress(Guid lessonId, Guid studentId);
        EProgressLesson GetProgressStatusLesson(Guid lessonId, Guid studentId);
    }
}
