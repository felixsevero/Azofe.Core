using Xunit;

namespace Azofe.Core.Tests;

public class EntityTests {

	[Fact]
	public void Construtor_DeveFalhar_ParaUmValorNulo() => Assert.Throws<ArgumentNullException>(() => new SimpleEntity(null!));

	[Fact]
	public void Equals_DeveSerFalso_ParaTiposDiferentes() {
		SimpleEntity simpleEntity = new(1);
		AnotherSimpleEntity anotherSimpleEntity = new(1);

		Assert.False(simpleEntity.Equals(anotherSimpleEntity));
	}

	[Fact]
	public void Equals_DeveSerFalso_ParaUmValorDiferente() {
		SimpleEntity simpleEntity = new(1);
		SimpleClass simpleClass = new();

		Assert.False(simpleEntity.Equals(simpleClass));
	}

	[Fact]
	public void Equals_DeveSerFalso_ParaUmValorNulo() {
		SimpleEntity simpleEntity = new(1);

		Assert.False(simpleEntity.Equals(null));
		Assert.False(simpleEntity == null);
		Assert.False(null == simpleEntity);
	}

	[Fact]
	public void Equals_DeveSerVerdadeiro_ParaObjetosIguais() {
		object simpleEntity1 = new SimpleEntity(1);
		object simpleEntity2 = new SimpleEntity(1);

		// Equals method is overridden and evaluated at runtime.
		Assert.True(simpleEntity1.Equals(simpleEntity2));

		// Equality operator (==) is overloaded and evaluated at compile time.
		// See: https://stackoverflow.com/a/1849288/2406622
		Assert.False(simpleEntity1 == simpleEntity2);
	}

	[Fact]
	public void Equals_DeveSerVerdadeiro_ParaUmValorReflexivo() {
		SimpleEntity simpleEntity = new(1);

		Assert.True(simpleEntity.Equals(simpleEntity));
	}

	[Fact]
	public void Equals_DeveSerVerdadeiro_ParaValoresSimetricos() {
		SimpleEntity simpleEntity1 = new(1);
		SimpleEntity simpleEntity2 = new(1);

		Assert.True(simpleEntity1.Equals(simpleEntity2));
		Assert.True(simpleEntity2.Equals(simpleEntity1));
	}

	[Fact]
	public void Equals_DeveSerVerdadeiro_ParaValoresTransitivos() {
		SimpleEntity simpleEntity1 = new(1);
		SimpleEntity simpleEntity2 = new(1);
		SimpleEntity simpleEntity3 = new(1);

		Assert.True(simpleEntity1.Equals(simpleEntity2));
		Assert.True(simpleEntity2.Equals(simpleEntity3));
		Assert.True(simpleEntity1.Equals(simpleEntity3));
	}

	[Fact]
	public void GetHashCode_DeveSerDiferente_ParaValoresDiferentes() {
		SimpleEntity simpleEntity1 = new(1);
		SimpleEntity simpleEntity2 = new(2);
		AnotherSimpleEntity anotherSimpleEntity = new(1);

		Assert.NotEqual(simpleEntity1.GetHashCode(), simpleEntity2.GetHashCode());
		Assert.NotEqual(simpleEntity1.GetHashCode(), anotherSimpleEntity.GetHashCode());
	}

	[Fact]
	public void GetHashCode_DeveSerIgual_ParaValoresIguais() {
		SimpleEntity simpleEntity1 = new(1);
		SimpleEntity simpleEntity2 = new(1);

		Assert.Equal(simpleEntity1.GetHashCode(), simpleEntity1.GetHashCode());
		Assert.Equal(simpleEntity1.GetHashCode(), simpleEntity2.GetHashCode());
	}

	class AnotherSimpleEntity: Entity {

		public AnotherSimpleEntity(Id id): base(id) {}

	}

	class SimpleClass {}

	class SimpleEntity: Entity {

		public SimpleEntity(Id id): base(id) {}

	}

}
