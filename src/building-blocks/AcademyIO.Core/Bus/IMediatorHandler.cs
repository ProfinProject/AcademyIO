using AcademyIO.Core.Messages;

namespace AcademyIO.Core.Bus
{
    public interface IMediatorHandler
    {
        Task PublicEvent<T>(T ev) where T : Event;
    }
}
