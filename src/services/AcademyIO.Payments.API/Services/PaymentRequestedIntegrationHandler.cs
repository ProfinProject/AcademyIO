using AcademyIO.Core.Messages.Integration;
using AcademyIO.Courses.API.Application.Commands;
using AcademyIO.MessageBus;
using FluentValidation.Results;
using MediatR;

namespace AcademyIO.Payments.API.Services
{
    public class PaymentRequestedIntegrationHandler: BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public PaymentRequestedIntegrationHandler(
                            IServiceProvider serviceProvider,
                            IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<PaymentRegisteredIntegrationEvent, ResponseMessage>(async request =>
                await RegisterPayment(request));

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

        private async Task<ResponseMessage> RegisterPayment(PaymentRegisteredIntegrationEvent message)
        {
            var command = new ValidatePaymentCourseCommand(message.CourseId, message.StudentId, message.CardName,
                                                        message.CardNumber, message.CardExpirationDate,
                                                        message.CardCVV);
            bool sucesso;
            ValidationResult validationResult = new();

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                sucesso = await mediator.Send(command);
            }
            if (!sucesso)
                validationResult.Errors.Add(new ValidationFailure() { ErrorMessage = "Falha ao realizar pagamento." });
            return new ResponseMessage(validationResult);
        }
    }
}
