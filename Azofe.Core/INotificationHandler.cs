namespace Azofe.Core;

public interface INotificationHandler<in TNotification> where TNotification: Notification {

	Task Handle(TNotification notification, CancellationToken cancellationToken);

}
