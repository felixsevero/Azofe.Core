namespace Azofe.Core;

public sealed class Id: ValueObject {

	public Id(int value) {
		if(!IsValid(value))
			throw new ArgumentException("The identifier must be a number greater than zero.", nameof(value));
		Value = value;
	}

	public static implicit operator Id(int value) => new(value);

	public static implicit operator int(Id id) => id.Value;

	public int Value { get; }

	public static bool IsValid(int value) => value > 0;

	protected override IEnumerable<object?> GetEqualityComponents() {
		yield return Value;
	}

	public override string ToString() => Value.ToString();

}
