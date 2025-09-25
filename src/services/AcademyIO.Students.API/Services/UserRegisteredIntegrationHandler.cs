using AcademyIO.Core.Messages.Integration;
using AcademyIO.MessageBus;
using AcademyIO.Students.API.Application.Commands;
using FluentValidation.Results;
using MediatR;

namespace AcademyIO.Students.API.Services
{
    public class UserRegisteredIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public UserRegisteredIntegrationHandler(
                            IServiceProvider serviceProvider,
                            IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request =>
                await RegisterStudent(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> RegisterStudent(UserRegisteredIntegrationEvent message)
        {
            var command = new AddUserCommand(message.Id, message.UserName, message.IsAdmin, message.FirstName, message.LastName, message.DateOfBirth, message.UserName);
            bool sucesso;
            ValidationResult validationResult = new();

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                sucesso = await mediator.Send(command);
            }
            if (!sucesso)
                validationResult.Errors.Add(new ValidationFailure() { ErrorMessage = "Falha ao cadastrar estudante" });
            return new ResponseMessage(validationResult);
        }
    }
}