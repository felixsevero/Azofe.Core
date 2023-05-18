using System.Reflection;

namespace Azofe.Core;

public class AuthorizationBehavior<TRequest, TResponse>: PipelineBehavior<TRequest, TResponse> where TRequest: notnull {

	const string Unauthenticated = $"[{nameof(Unauthenticated)}]";
	const string Unauthorized = $"[{nameof(Unauthorized)}]";

	readonly IApplicationUserAuthorizer applicationUserAuthorizer;
	readonly IApplicationUserProvider applicationUserProvider;

	public AuthorizationBehavior(IApplicationUserAuthorizer applicationUserAuthorizer, IApplicationUserProvider applicationUserProvider) {
		this.applicationUserAuthorizer = applicationUserAuthorizer;
		this.applicationUserProvider = applicationUserProvider;
	}

	public override async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
		PermissionAttribute? permissionAttribute = request.GetType().GetCustomAttribute<PermissionAttribute>();
		if(permissionAttribute is null)
			throw new InvalidOperationException($"The {nameof(PermissionAttribute)} attribute must be assigned to {typeof(TRequest)}.");
		if(permissionAttribute.Permission == PermissionAttribute.AllowAnyone)
			return await next();
		IApplicationUser? applicationUser = await applicationUserProvider.GetApplicationUserAsync();
		if(applicationUser is null)
			return GetResponseWithError(Unauthenticated, "The user is not authenticated.");
		if(!await applicationUserAuthorizer.AuthorizeAsync(applicationUser, permissionAttribute.Permission))
			return GetResponseWithError(Unauthorized, "The user is not authorized.");
		return await next();

		static TResponse GetResponseWithError(string code, string description) {
			TResponse response = Activator.CreateInstance<TResponse>();
			if(response is not Result result)
				throw new InvalidCastException($"The type {typeof(TResponse)} cannot be cast to {typeof(Result)}.");
			result.AddError(code, description);
			return response;
		}
	}

}
