namespace Azofe.Core;

public interface IRequestHandler<in TRequest>: MediatR.IRequestHandler<TRequest, Result> where TRequest: IRequest {}

public interface IRequestHandler<in TRequest, TResponse>: MediatR.IRequestHandler<TRequest, Result<TResponse>> where TRequest: IRequest<TResponse> {}
