namespace Azofe.Core;

public static class ResultExtensions {

	public static void Try(this Result result, Action action) {
		try {
			action();
		}
		catch(DomainException e) {
			result.AddErrors(e.Messages.Select(m => new ResultError(null, m)));
		}
		catch(InfrastructureException e) {
			result.AddError(null, e.Message);
		}
	}

	public static T? Try<T>(this Result result, Func<T?> func) {
		try {
			return func();
		}
		catch(DomainException e) {
			result.AddErrors(e.Messages.Select(m => new ResultError(null, m)));
		}
		catch(InfrastructureException e) {
			result.AddError(null, e.Message);
		}
		return default;
	}

	public static async Task TryAsync(this Result result, Func<Task> func) {
		try {
			await func();
		}
		catch(DomainException e) {
			result.AddErrors(e.Messages.Select(m => new ResultError(null, m)));
		}
		catch(InfrastructureException e) {
			result.AddError(null, e.Message);
		}
	}

	public static async Task<T?> TryAsync<T>(this Result result, Func<Task<T?>> func) {
		try {
			return await func();
		}
		catch(DomainException e) {
			result.AddErrors(e.Messages.Select(m => new ResultError(null, m)));
		}
		catch(InfrastructureException e) {
			result.AddError(null, e.Message);
		}
		return default;
	}

}
