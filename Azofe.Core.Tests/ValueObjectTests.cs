using Xunit;

namespace Azofe.Core.Tests;

public class ValueObjectTests {

	[Fact]
	public void Equals_EquivalentCollectionValueObject_ReturnsTrue() {
		CollectionValueObject collectionValueObject1 = new(new SimpleValueObject[] { new(1), new(2) });
		CollectionValueObject collectionValueObject2 = new(new SimpleValueObject[] { new(1), new(2) });

		bool actual = collectionValueObject1.Equals(collectionValueObject2);

		Assert.True(actual);
	}

	[Fact]
	public void Equals_EquivalentNestedValueObject_ReturnsTrue() {
		NestedValueObject nestedValueObject1 = new(new SimpleValueObject(1));
		NestedValueObject nestedValueObject2 = new(new SimpleValueObject(1));

		bool actual = nestedValueObject1.Equals(nestedValueObject2);

		Assert.True(actual);
	}

	[Fact]
	public void Equals_EquivalentObject_ReturnsTrue() {
		object simpleValueObject1 = new SimpleValueObject(1);
		object simpleValueObject2 = new SimpleValueObject(1);

		bool actual = simpleValueObject1.Equals(simpleValueObject2);

		// Equals method is overridden and evaluated at runtime.
		Assert.True(actual);

		// Equality operator (==) is overloaded and evaluated at compile time.
		// See: https://stackoverflow.com/a/1849288/2406622
		Assert.False(simpleValueObject1 == simpleValueObject2);
	}

	[Fact]
	public void Equals_NotEquivalentCollectionValueObject_ReturnsFalse() {
		CollectionValueObject collectionValueObject1 = new(new SimpleValueObject[] { new(1), new(2) });
		CollectionValueObject collectionValueObject2 = new(new SimpleValueObject[] { new(1), new(3) });
		CollectionValueObject collectionValueObject3 = new(new SimpleValueObject[] { new(1), new(2), new(3) });

		Assert.False(collectionValueObject1.Equals(collectionValueObject2));
		Assert.False(collectionValueObject1.Equals(collectionValueObject3));
	}

	[Fact]
	public void Equals_NotEquivalentNestedValueObject_ReturnsFalse() {
		NestedValueObject nestedValueObject1 = new(new SimpleValueObject(1));
		NestedValueObject nestedValueObject2 = new(new SimpleValueObject(2));

		bool actual = nestedValueObject1.Equals(nestedValueObject2);

		Assert.False(actual);
	}

	[Fact]
	public void Equals_NotEquivalentValueObject_ReturnsFalse() {
		SimpleValueObject simpleValueObject = new(1);
		AnotherSimpleValueObject anotherSimpleValueObject = new(1);

		bool actual = simpleValueObject.Equals(anotherSimpleValueObject);

		Assert.False(actual);
	}

	[Fact]
	public void Equals_NullArg_ReturnsFalse() {
		SimpleValueObject simpleValueObject = new(1);

		bool actual = simpleValueObject.Equals(null);

		Assert.False(actual);
		Assert.False(simpleValueObject == null);
		Assert.False(null == simpleValueObject);
	}

	[Fact]
	public void Equals_ReflexiveValue_ReturnsTrue() {
		SimpleValueObject simpleValueObject = new(1);

		bool actual = simpleValueObject.Equals(simpleValueObject);

		Assert.True(actual);
	}

	[Fact]
	public void Equals_SymmetricValue_ReturnsTrue() {
		SimpleValueObject simpleValueObject1 = new(1);
		SimpleValueObject simpleValueObject2 = new(1);

		Assert.True(simpleValueObject1.Equals(simpleValueObject2));
		Assert.True(simpleValueObject2.Equals(simpleValueObject1));
	}

	[Fact]
	public void Equals_TransitiveValue_ReturnsTrue() {
		SimpleValueObject simpleValueObject1 = new(1);
		SimpleValueObject simpleValueObject2 = new(1);
		SimpleValueObject simpleValueObject3 = new(1);

		Assert.True(simpleValueObject1.Equals(simpleValueObject2));
		Assert.True(simpleValueObject2.Equals(simpleValueObject3));
		Assert.True(simpleValueObject1.Equals(simpleValueObject3));
	}

	[Fact]
	public void GetHashCode_EquivalentValueObject_SameHash() {
		SimpleValueObject simpleValueObject1 = new(1);
		SimpleValueObject simpleValueObject2 = new(1);

		Assert.Equal(simpleValueObject1.GetHashCode(), simpleValueObject1.GetHashCode());
		Assert.Equal(simpleValueObject1.GetHashCode(), simpleValueObject2.GetHashCode());
	}

	[Fact]
	public void GetHashCode_NotEquivalentValueObject_NotSameHash() {
		SimpleValueObject simpleValueObject1 = new(1);
		SimpleValueObject simpleValueObject2 = new(2);

		Assert.NotEqual(simpleValueObject1.GetHashCode(), simpleValueObject2.GetHashCode());
	}

	class AnotherSimpleValueObject: SimpleValueObject {

		public AnotherSimpleValueObject(int value): base(value) {}

	}

	class CollectionValueObject: ValueObject {

		readonly IEnumerable<SimpleValueObject> simpleValueObjects;

		public CollectionValueObject(IEnumerable<SimpleValueObject> simpleValueObjects) => this.simpleValueObjects = simpleValueObjects;

		protected override IEnumerable<object?> GetEqualityComponents() {
			foreach(SimpleValueObject simpleValueObject in simpleValueObjects)
				yield return simpleValueObject;
		}

	}

	class NestedValueObject: ValueObject {

		readonly SimpleValueObject simpleValueObject;

		public NestedValueObject(SimpleValueObject simpleValueObject) => this.simpleValueObject = simpleValueObject;

		protected override IEnumerable<object?> GetEqualityComponents() {
			yield return simpleValueObject;
		}

	}

	class SimpleValueObject: ValueObject {

		readonly int value;

		public SimpleValueObject(int value) => this.value = value;

		protected override IEnumerable<object?> GetEqualityComponents() {
			yield return value;
		}

	}

}
