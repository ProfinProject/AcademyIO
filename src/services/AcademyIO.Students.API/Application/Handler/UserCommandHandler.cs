using AcademyIO.Core.Messages;
using AcademyIO.Core.Messages.Notifications;
using AcademyIO.Core.Models;
using AcademyIO.ManagementStudents.Application.Commands;
using AcademyIO.ManagementStudents.Models;
using MediatR;

namespace FabianoIO.ManagementStudents.Application.Handler
{
    public class UserCommandHandler(IMediator mediator
                                   , IUserRepository userRepository) : IRequestHandler<AddUserCommand, bool>
    {
        public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request))
                return false;

            var user = new User(new Guid(), request.Name, request.LastName, request.Email, request.DateOfBirth);

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
