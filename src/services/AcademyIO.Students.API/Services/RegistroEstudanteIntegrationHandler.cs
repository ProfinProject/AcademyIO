using AcademyIO.Core.Messages.Integration;
using AcademyIO.ManagementStudents.Application.Commands;
using AcademyIO.MessageBus;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Services
{
    public class RegistroEstudanteIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegistroEstudanteIntegrationHandler(
                            IServiceProvider serviceProvider,
                            IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request =>
                await RegistrarCliente(request));

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

        private async Task<ResponseMessage> RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
        {
            //Todo: Verificar os dados que vem da mensagem para criação do usuário.
            var estudanteCommand = new AddUserCommand(message.Nome, false, message.Nome, message.Nome, DateTime.Now, message.Email);
            bool sucesso;
            ValidationResult  validationResult = new();

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                sucesso = await mediator.Send(estudanteCommand);
            }
            if (!sucesso)
                validationResult.Errors.Add(new ValidationFailure() { ErrorMessage = "Falha ao cadastrar estudante" });
            return new ResponseMessage(validationResult);
        }
    }
}