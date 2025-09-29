using AcademyIO.Core.DomainObjects.DTOs;
using AcademyIO.Core.Messages;
using AcademyIO.Core.Messages.IntegrationCommands;
using AcademyIO.Core.Messages.Notifications;
using AcademyIO.Courses.API.Application.Commands;
using AcademyIO.Courses.API.Models;
using MediatR;

namespace AcademyIO.Courses.API.Application.Handlers
{
    public class CourseCommandHandler(ICourseRepository courseRepository,
                                ILessonRepository lessonRepository,
                                IMediator mediator) : IRequestHandler<AddCourseCommand, bool>,
                                                      IRequestHandler<UpdateCourseCommand, bool>,
                                                      IRequestHandler<RemoveCourseCommand, bool>,
                                                      IRequestHandler<ValidatePaymentCourseCommand, bool>,
                                                      IRequestHandler<CreateProgressByCourseCommand, bool>
    {
        public async Task<bool> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            if (!ValidatComand(request)) return false;

            var course = new Course
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            courseRepository.Add(course);

            return await courseRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            if (!ValidatComand(request)) return false;

            var course = await courseRepository.GetById(request.CourseId);
            if (course == null)
            {
                await mediator.Publish(new DomainNotification(request.MessageType, "Curso não encontrado."), cancellationToken);
                return false;
            }

            course.Id = request.CourseId;
            course.Name = request.Name;
            course.Description = request.Description;
            course.Price = request.Price;

            courseRepository.Update(course);

            return await courseRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoveCourseCommand request, CancellationToken cancellationToken)
        {
            if (!ValidatComand(request)) return false;

            var course = await courseRepository.GetById(request.CourseId);
            if (course == null)
            {
                await mediator.Publish(new DomainNotification(request.MessageType, "Curso não encontrado."), cancellationToken);
                return false;
            }

            courseRepository.Delete(course);

            return await courseRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ValidatePaymentCourseCommand command, CancellationToken cancellationToken)
        {
            if (!ValidatComand(command))
                return false;

            var course = await courseRepository.GetById(command.CourseId);
            if (course == null)
            {
                await mediator.Publish(new DomainNotification(command.MessageType, "Curso não encontrado."), cancellationToken);
                return false;
            }

            var paymentCourse = new PaymentCourse
            {
                StudentId = command.StudentId,
                CourseId = course.Id,
                CardCVV = command.CardCVV,
                CardExpirationDate = command.CardExpirationDate,
                CardName = command.CardName,
                CardNumber = command.CardNumber,
                Total = course.Price
            };

            return await mediator.Send(new MakePaymentCourseCommand(paymentCourse), cancellationToken);
        }

        public async Task<bool> Handle(CreateProgressByCourseCommand request, CancellationToken cancellationToken)
        {
            if (!ValidatComand(request)) return false;

            await lessonRepository.CreateProgressLessonByCourse(request.CourseId, request.StudentId);

            return await lessonRepository.UnitOfWork.Commit();
        }

        private bool ValidatComand(Command command)
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
