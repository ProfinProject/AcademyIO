using AcademyIO.Core.Notifications;

namespace AcademyIO.Core.Interfaces.Services
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
