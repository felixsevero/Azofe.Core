namespace Azofe.Core;

public interface IApplicationUserProvider {

	Task<IApplicationUser?> GetApplicationUserAsync();

}
