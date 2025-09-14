namespace AcademyIO.Courses.API.Application.Queries.ViewModels
{
    public class LessonViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public double TotalHours { get; set; }
        public Guid CourseId { get; set; }
    }
}
