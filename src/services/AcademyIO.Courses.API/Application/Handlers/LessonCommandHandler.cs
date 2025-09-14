using AcademyIO.Core.Messages;
using AcademyIO.Core.Messages.Notifications;
using AcademyIO.Courses.API.Application.Commands;
using AcademyIO.Courses.API.Models;
using MediatR;

namespace AcademyIO.Courses.API.Application.Handlers
{
    public class LessonCommandHandler(ILessonRepository lessonRepository,
                                IMediator mediator) : IRequestHandler<AddLessonCommand, bool>,
                                                    IRequestHandler<StartLessonCommand, bool>,
                                                    IRequestHandler<FinishLessonCommand, bool>
    {
        public async Task<bool> Handle(AddLessonCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateComand(request)) return false;

            var lesson = new Lesson(request.Name, request.Subject, request.TotalHours, request.CourseId);

            lessonRepository.Add(lesson);

            return await lessonRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(StartLessonCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateComand(request)) return false;

            await lessonRepository.StartLesson(request.LessonId, request.StudentId);

            return await lessonRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(FinishLessonCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateComand(request)) return false;

            await lessonRepository.FinishLesson(request.LessonId, request.StudentId);

            return await lessonRepository.UnitOfWork.Commit();
        }

        private bool ValidateComand(Command command)
        {
            if (command.IsValid()) return true;

            foreach (var erro in command.ValidationResult.Errors)
            {
                mediator.Publish(new DomainNotification(command.MessageType, erro.ErrorMessage));
            }

            return false;
        }
    }
}
