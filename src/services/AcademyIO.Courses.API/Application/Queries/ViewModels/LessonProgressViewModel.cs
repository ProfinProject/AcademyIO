namespace AcademyIO.Courses.API.Application.Queries.ViewModels
{
    public class LessonProgressViewModel(string lessonName, string progressLesson)
    {
        public string LessonName { get; } = lessonName;
        public string ProgressLesson { get; } = progressLesson;
    }
}
