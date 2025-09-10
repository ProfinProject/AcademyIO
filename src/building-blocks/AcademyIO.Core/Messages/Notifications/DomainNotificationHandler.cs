using MediatR;

namespace AcademyIO.Core.Messages.Notifications;

public class DomainNotificationHandler : INotificationHandler<DomainNotification>
{
    private List<DomainNotification> _notifications;

    public DomainNotificationHandler()
    {
        _notifications = [];
    }
    public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
    {
        _notifications.Add(notification);
        return Task.CompletedTask;
    }

    public virtual List<DomainNotification> GetNotifications()
    {
        return _notifications;
    }

    public virtual bool HasNotification()
    {
        return GetNotifications().Any();
    }

    public void Dispose()
    {
        _notifications = [];
    }
}