using AcademyIO.Core.DomainObjects;

namespace AcademyIO.Courses.API.Models
{
    public class Course : Entity, IAggregateRoot
    {
        public Course() : base()
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        private readonly List<Lesson> _lessons;
        public IReadOnlyCollection<Lesson> Lessons => _lessons;

        public void AddLesson(Lesson lesson)
        {
            //TO DO validate if exists and add lesson throw exception
        }

        private bool LessonExistis(Lesson lesson)
        {
            //TO DO validate if exists
            return false;
        }
    }
}
