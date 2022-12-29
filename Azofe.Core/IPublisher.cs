namespace Azofe.Core;

public interface IPublisher {

	Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification: Notification;

}
