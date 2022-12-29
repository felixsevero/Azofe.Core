namespace Azofe.Core;

public class Mediator: IPublisher, ISender {

	readonly MediatR.IMediator mediator;

	public Mediator(MediatR.IMediator mediator) => this.mediator = mediator;

	public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification: Notification => mediator.Publish(notification, cancellationToken);

	public Task<Result> Send(IRequest request, CancellationToken cancellationToken = default) => mediator.Send(request, cancellationToken);

	public Task<Result<TResponse>> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) => mediator.Send(request, cancellationToken);

}
