namespace Azofe.Core;

public class DomainException: Exception {

	public DomainException(): this(Enumerable.Empty<string>(), null) {}

	public DomainException(params string[] messages): this(messages, null) {}

	public DomainException(IEnumerable<string> messages): this(messages, null) {}

	public DomainException(IEnumerable<string> messages, Exception? innerException): base("Uma exceção de domínio foi gerada. Veja as mensagens para mais detalhes.", innerException) => Messages.AddRange(messages);

	public List<string> Messages { get; } = new List<string>();

}
