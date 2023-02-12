namespace Azofe.Core;

public class DomainException: Exception {

	public DomainException(): this(Enumerable.Empty<string>(), null) {}

	public DomainException(params string[] messages): this(messages, null) {}

	public DomainException(IEnumerable<string> messages): this(messages, null) {}

	public DomainException(IEnumerable<string> messages, Exception? innerException): base($"A domain exception was thrown. See the {nameof(Messages)} for more details.", innerException) => Messages.AddRange(messages);

	public List<string> Messages { get; } = new List<string>();

}
