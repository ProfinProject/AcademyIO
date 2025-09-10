using AcademyIO.Core.Messages;
using MediatR;

namespace AcademyIO.Core.Bus
{
    public class MediatrHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatrHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublicEvent<T>(T ev) where T : Event
        {
            await _mediator.Publish(ev);
        }
    }
}
