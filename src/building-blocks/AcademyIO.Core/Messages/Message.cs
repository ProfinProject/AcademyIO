namespace AcademyIO.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public Guid AggregatedId { get; protected set; }
        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
