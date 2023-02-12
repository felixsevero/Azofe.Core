using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Azofe.Core;

public class RequestPerformanceBehavior<TRequest, TResponse>: MediatR.IPipelineBehavior<TRequest, TResponse> where TRequest: MediatR.IRequest<TResponse> {

	const int MillisecondsLimit = 1000;

	readonly ILogger<RequestPerformanceBehavior<TRequest, TResponse>> logger;
	readonly Stopwatch stopWatch;

	public RequestPerformanceBehavior(ILogger<RequestPerformanceBehavior<TRequest, TResponse>> logger) {
		this.logger = logger;
		stopWatch = new Stopwatch();
	}

	public async Task<TResponse> Handle(TRequest request, MediatR.RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
		stopWatch.Start();
		TResponse response = await next();
		stopWatch.Stop();
		TimeSpan elapsedTime = stopWatch.Elapsed;
		if(elapsedTime > TimeSpan.FromMilliseconds(MillisecondsLimit))
			logger.LogWarning("The request [{TRequest}]:[{TResponse}] took [{ElapsedTime}] to complete.", typeof(TRequest), typeof(TResponse), elapsedTime);
		return response;
	}

}
