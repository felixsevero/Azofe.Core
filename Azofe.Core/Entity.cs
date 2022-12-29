namespace Azofe.Core;

public abstract class Entity {

	protected Entity(Id id) => Id = id ?? throw new ArgumentNullException(nameof(id));

	public Id Id { get; }

	public override bool Equals(object? obj) {
		if(obj is not Entity entity)
			return false;
		if(ReferenceEquals(this, entity))
			return true;
		if(GetType() != entity.GetType())
			return false;
		return Id == entity.Id;
	}

	public override int GetHashCode() {
		HashCode hashCode = new();
		hashCode.Add(GetType());
		hashCode.Add(Id);
		return hashCode.ToHashCode();
	}

	public override string ToString() => $"Id = {Id}";

	public static bool operator ==(Entity? a, Entity? b) {
		if(a is null && b is null)
			return true;
		if(a is null || b is null)
			return false;
		return a.Equals(b);
	}

	public static bool operator !=(Entity? a, Entity? b) => !(a == b);

}
