using AcademyIO.Core.Messages;
using AcademyIO.Core.Messages.Notifications;
using AcademyIO.Students.API.Application.Commands;
using AcademyIO.Students.API.Models;
using MediatR;

namespace AcademyIO.Students.API.Application.Handler
{
    public class UserCommandHandler(IMediator mediator
                                   , IUserRepository userRepository) : IRequestHandler<AddUserCommand, bool>
    {
        public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request))
                return false;

            var user = new StudentUser(request.UserId, request.UserName, request.Name, request.LastName, request.Email, request.DateOfBirth, request.IsAdmin);

            userRepository.Add(user);
            return await userRepository.UnitOfWork.Commit();
        }

        private bool ValidateCommand(Command command)
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
