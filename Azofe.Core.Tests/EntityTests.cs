using Xunit;

namespace Azofe.Core.Tests;

public class EntityTests {

	[Fact]
	public void Constructor_NullArg_ThrowsArgumentNullException() {
		Action actual = () => new SimpleEntity(null!);

		Assert.Throws<ArgumentNullException>(actual);
	}

	[Fact]
	public void Equals_EquivalentObject_ReturnsTrue() {
		object simpleEntity1 = new SimpleEntity(1);
		object simpleEntity2 = new SimpleEntity(1);

		bool actual = simpleEntity1.Equals(simpleEntity2);

		// Equals method is overridden and evaluated at runtime.
		Assert.True(actual);

		// Equality operator (==) is overloaded and evaluated at compile time.
		// See: https://stackoverflow.com/a/1849288/2406622
		Assert.False(simpleEntity1 == simpleEntity2);
	}

	[Fact]
	public void Equals_NotEntity_ReturnsFalse() {
		SimpleEntity simpleEntity = new(1);
		SimpleClass simpleClass = new();

		bool actual = simpleEntity.Equals(simpleClass);

		Assert.False(actual);
	}

	[Fact]
	public void Equals_NotEquivalentEntity_ReturnsFalse() {
		SimpleEntity simpleEntity = new(1);
		AnotherSimpleEntity anotherSimpleEntity = new(1);

		bool actual = simpleEntity.Equals(anotherSimpleEntity);

		Assert.False(actual);
	}

	[Fact]
	public void Equals_NullArg_ReturnsFalse() {
		SimpleEntity simpleEntity = new(1);

		bool actual = simpleEntity.Equals(null);

		Assert.False(actual);
		Assert.False(simpleEntity == null);
		Assert.False(null == simpleEntity);
	}

	[Fact]
	public void Equals_ReflexiveValue_ReturnsTrue() {
		SimpleEntity simpleEntity = new(1);

		bool actual = simpleEntity.Equals(simpleEntity);

		Assert.True(actual);
	}

	[Fact]
	public void Equals_SymmetricValue_ReturnsTrue() {
		SimpleEntity simpleEntity1 = new(1);
		SimpleEntity simpleEntity2 = new(1);

		Assert.True(simpleEntity1.Equals(simpleEntity2));
		Assert.True(simpleEntity2.Equals(simpleEntity1));
	}

	[Fact]
	public void Equals_TransitiveValue_ReturnsTrue() {
		SimpleEntity simpleEntity1 = new(1);
		SimpleEntity simpleEntity2 = new(1);
		SimpleEntity simpleEntity3 = new(1);

		Assert.True(simpleEntity1.Equals(simpleEntity2));
		Assert.True(simpleEntity2.Equals(simpleEntity3));
		Assert.True(simpleEntity1.Equals(simpleEntity3));
	}

	[Fact]
	public void GetHashCode_EquivalentEntity_SameHash() {
		SimpleEntity simpleEntity1 = new(1);
		SimpleEntity simpleEntity2 = new(1);

		Assert.Equal(simpleEntity1.GetHashCode(), simpleEntity1.GetHashCode());
		Assert.Equal(simpleEntity1.GetHashCode(), simpleEntity2.GetHashCode());
	}

	[Fact]
	public void GetHashCode_NotEquivalentEntity_NotSameHash() {
		SimpleEntity simpleEntity1 = new(1);
		SimpleEntity simpleEntity2 = new(2);
		AnotherSimpleEntity anotherSimpleEntity = new(1);

		Assert.NotEqual(simpleEntity1.GetHashCode(), simpleEntity2.GetHashCode());
		Assert.NotEqual(simpleEntity1.GetHashCode(), anotherSimpleEntity.GetHashCode());
	}

	class AnotherSimpleEntity: Entity {

		public AnotherSimpleEntity(Id id): base(id) {}

	}

	class SimpleClass {}

	class SimpleEntity: Entity {

		public SimpleEntity(Id id): base(id) {}

	}

}
