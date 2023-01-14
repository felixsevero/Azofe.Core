namespace Azofe.Core;

public sealed class ConcurrencyToken: ValueObject {

	public ConcurrencyToken(uint value) => Value = value;

	public static implicit operator ConcurrencyToken(uint value) => new(value);

	public static implicit operator uint(ConcurrencyToken concurrencyToken) => concurrencyToken.Value;

	public uint Value { get; }

	protected override IEnumerable<object?> GetEqualityComponents() {
		yield return Value;
	}

	public override string ToString() => Value.ToString();

}
