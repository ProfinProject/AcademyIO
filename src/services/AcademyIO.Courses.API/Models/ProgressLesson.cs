using AcademyIO.Core.DomainObjects;
using AcademyIO.Core.Enums;

namespace AcademyIO.Courses.API.Models
{
    public class ProgressLesson : Entity, IAggregateRoot
    {
        public ProgressLesson(Guid lessonId, Guid studentId, EProgressLesson progressLesson)
        {
            this.LessonId = lessonId;
            this.StudentId = studentId;
            this.ProgressionStatus = progressLesson;
        }

        public ProgressLesson() { }

        public Guid StudentId { get; set; }
        public Guid LessonId { get; set; }
        public EProgressLesson ProgressionStatus { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
