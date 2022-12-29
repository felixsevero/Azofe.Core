namespace Azofe.Core;

public interface ISender {

	Task<Result> Send(IRequest request, CancellationToken cancellationToken = default);

	Task<Result<TResponse>> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

}
