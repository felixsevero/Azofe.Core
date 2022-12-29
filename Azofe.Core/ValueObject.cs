namespace Azofe.Core;

public abstract class ValueObject {

	public override bool Equals(object? obj) {
		if(obj is null)
			return false;
		if(GetType() != obj.GetType())
			return false;
		ValueObject valueObject = (ValueObject)obj;
		return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
	}

	protected abstract IEnumerable<object?> GetEqualityComponents();

	public override int GetHashCode() {
		HashCode hashCode = new();
		foreach(object? obj in GetEqualityComponents())
			hashCode.Add(obj);
		return hashCode.ToHashCode();
	}

	public static bool operator ==(ValueObject? a, ValueObject? b) {
		if(a is null && b is null)
			return true;
		if(a is null || b is null)
			return false;
		return a.Equals(b);
	}

	public static bool operator !=(ValueObject? a, ValueObject? b) => !(a == b);

}
