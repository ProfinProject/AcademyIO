using MediatR;
using AcademyIO.Core.Messages.IntegrationCommands;

namespace AcademyIO.Payments.API.Business.Handlers;

public class PaymentCommandHandler(IPaymentService paymentService) : IRequestHandler<MakePaymentCourseCommand, bool>
{
    public async Task<bool> Handle(MakePaymentCourseCommand request, CancellationToken cancellationToken)
    {
        return await paymentService.MakePaymentCourse(request.PaymentCourse);
    }
}