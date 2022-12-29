namespace Azofe.Core;

public interface IRequest: MediatR.IRequest<Result> {}

public interface IRequest<TResponse>: MediatR.IRequest<Result<TResponse>> {}
