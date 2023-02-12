using Microsoft.Extensions.Logging;

namespace Azofe.Core;

public class UnhandledExceptionBehavior<TRequest, TResponse>: MediatR.IPipelineBehavior<TRequest, TResponse> where TRequest: MediatR.IRequest<TResponse> {

	readonly ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger;

	public UnhandledExceptionBehavior(ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger) => this.logger = logger;

	public async Task<TResponse> Handle(TRequest request, MediatR.RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
		try {
			return await next();
		}
		catch(Exception e) {
			logger.LogError(e, "The request [{TRequest}]:[{TResponse}] threw an exception of type [{TException}].", typeof(TRequest), typeof(TResponse), e.GetType());

			throw;
		}
	}

}
