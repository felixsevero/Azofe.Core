using System.ComponentModel.DataAnnotations;

namespace Azofe.Core;

public class RequestValidationBehavior<TRequest, TResponse>: MediatR.IPipelineBehavior<TRequest, TResponse> where TRequest: MediatR.IRequest<TResponse> {

	public async Task<TResponse> Handle(TRequest request, MediatR.RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
		List<ValidationResult> errors = new();
		if(Validator.TryValidateObject(request, new ValidationContext(request), errors, true))
			return await next();
		TResponse response = Activator.CreateInstance<TResponse>();
		if(response is not Result result)
			throw new InvalidCastException($"The type {typeof(TResponse)} cannot be cast to {typeof(Result)}.");
		foreach(ValidationResult error in errors)
			result.AddError(error.MemberNames.Single(), error.ErrorMessage ?? "No error message defined.");
		return response;
	}

}
