namespace Azofe.Core;

public sealed class Id: ValueObject {

	public Id(long value) {
		if(!IsValid(value))
			throw new ArgumentException("A identidade deve ser maior que zero.", nameof(value));
		Value = value;
	}

	public static implicit operator Id(long value) => new(value);

	public static implicit operator long(Id id) => id.Value;

	public long Value { get; }

	public static bool IsValid(long value) => value > 0;

	protected override IEnumerable<object?> GetEqualityComponents() {
		yield return Value;
	}

	public override string ToString() => Value.ToString(@"0\.000\.000\.000\.000\.000\.000");

}
