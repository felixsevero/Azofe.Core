namespace Azofe.Core;

public class SystemTimeProvider: ITimeProvider {

	public DateTime GetCurrentTime() => DateTime.UtcNow;

}
