namespace Azofe.Core;

public interface IApplicationUserAuthorizer {

	Task<bool> AuthorizeAsync(IApplicationUser applicationUser, string permission);

}
