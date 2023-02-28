namespace Azofe.Core;

public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();

public abstract class PipelineBehavior<TRequest, TResponse>: MediatR.IPipelineBehavior<TRequest, TResponse> where TRequest: notnull {

	Task<TResponse> MediatR.IPipelineBehavior<TRequest, TResponse>.Handle(TRequest request, MediatR.RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) => Handle(request, new RequestHandlerDelegate<TResponse>(next), cancellationToken);

	public abstract Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);

}
