using System.Collections.ObjectModel;
using System.Reflection;

namespace Azofe.Core;

public abstract class Enumeration {

	static readonly IDictionary<Type, IEnumerable<Enumeration>> Values;

	static Enumeration() => Values = new Dictionary<Type, IEnumerable<Enumeration>>();

	protected Enumeration(int value, string name) {
		ArgumentNullException.ThrowIfNull(name);
		Value = value;
		Name = name;
	}

	public string Name { get; }

	public int Value { get; }

	public override bool Equals(object? obj) {
		if(obj is not Enumeration other)
			return false;
		if(ReferenceEquals(this, other))
			return true;
		if(GetType() != other.GetType())
			return false;
		return Value == other.Value;
	}

	public override int GetHashCode() {
		HashCode hashCode = new();
		hashCode.Add(GetType());
		hashCode.Add(Value);
		return hashCode.ToHashCode();
	}

	public static IEnumerable<T> GetValues<T>() where T: Enumeration {
		Type type = typeof(T);
		if(!Values.ContainsKey(type)) {
			T[] items = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly).Select(x => x.GetValue(null)).Cast<T>().ToArray();
			ReadOnlyCollection<T> collection = new(items);
			Values.Add(type, collection);
		}
		return (IEnumerable<T>)Values[type];
	}

	public static T Parse<T>(string name) where T: Enumeration => Parse<T, string>(name, "name", item => item.Name == name);

	public static T Parse<T>(int value) where T: Enumeration => Parse<T, int>(value, "value", item => item.Value == value);

	static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T: Enumeration {
		T? item = GetValues<T>().FirstOrDefault(predicate);
		if(item is null)
			throw new FormatException($"The parsing failed. The {description} '{value}' was not found in {typeof(T)}.");
		return item;
	}

	public override string ToString() => $"{Value} - {Name}";

	public static bool operator ==(Enumeration? a, Enumeration? b) {
		if(a is null && b is null)
			return true;
		if(a is null || b is null)
			return false;
		return a.Equals(b);
	}

	public static bool operator !=(Enumeration? a, Enumeration? b) => !(a == b);

}
