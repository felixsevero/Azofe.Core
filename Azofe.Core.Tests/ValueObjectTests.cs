using Xunit;

namespace Azofe.Core.Tests;

public class ValueObjectTests {

	[Fact]
	public void Equals_DeveSerFalso_ParaColecoesAninhadasDiferentes() {
		CollectionValueObject collectionValueObject1 = new(new SimpleValueObject[] { new(1), new(2) });
		CollectionValueObject collectionValueObject2 = new(new SimpleValueObject[] { new(1), new(3) });
		CollectionValueObject collectionValueObject3 = new(new SimpleValueObject[] { new(1), new(2), new(3) });

		Assert.False(collectionValueObject1.Equals(collectionValueObject2));
		Assert.False(collectionValueObject1.Equals(collectionValueObject3));
	}

	[Fact]
	public void Equals_DeveSerFalso_ParaTiposDiferentes() {
		SimpleValueObject simpleValueObject = new(1);
		AnotherSimpleValueObject anotherSimpleValueObject = new(1);

		Assert.False(simpleValueObject.Equals(anotherSimpleValueObject));
	}

	[Fact]
	public void Equals_DeveSerFalso_ParaUmValorNulo() {
		SimpleValueObject simpleValueObject = new(1);

		Assert.False(simpleValueObject.Equals(null));
		Assert.False(simpleValueObject == null);
		Assert.False(null == simpleValueObject);
	}

	[Fact]
	public void Equals_DeveSerFalso_ParaValoresAninhadosDiferentes() {
		NestedValueObject nestedValueObject1 = new(new SimpleValueObject(1));
		NestedValueObject nestedValueObject2 = new(new SimpleValueObject(2));

		Assert.False(nestedValueObject1.Equals(nestedValueObject2));
	}

	[Fact]
	public void Equals_DeveSerVerdadeiro_ParaColecoesAninhadasIguais() {
		CollectionValueObject collectionValueObject1 = new(new SimpleValueObject[] { new(1), new(2) });
		CollectionValueObject collectionValueObject2 = new(new SimpleValueObject[] { new(1), new(2) });

		Assert.True(collectionValueObject1.Equals(collectionValueObject2));
	}

	[Fact]
	public void Equals_DeveSerVerdadeiro_ParaObjetosIguais() {
		object simpleValueObject1 = new SimpleValueObject(1);
		object simpleValueObject2 = new SimpleValueObject(1);

		// Equals method is overridden and evaluated at runtime.
		Assert.True(simpleValueObject1.Equals(simpleValueObject2));

		// Equality operator (==) is overloaded and evaluated at compile time.
		// See: https://stackoverflow.com/a/1849288/2406622
		Assert.False(simpleValueObject1 == simpleValueObject2);
	}

	[Fact]
	public void Equals_DeveSerVerdadeiro_ParaUmValorReflexivo() {
		SimpleValueObject simpleValueObject = new(1);

		Assert.True(simpleValueObject.Equals(simpleValueObject));
	}

	[Fact]
	public void Equals_DeveSerVerdadeiro_ParaValoresAninhadosIguais() {
		NestedValueObject nestedValueObject1 = new(new SimpleValueObject(1));
		NestedValueObject nestedValueObject2 = new(new SimpleValueObject(1));

		Assert.True(nestedValueObject1.Equals(nestedValueObject2));
	}

	[Fact]
	public void Equals_DeveSerVerdadeiro_ParaValoresSimetricos() {
		SimpleValueObject simpleValueObject1 = new(1);
		SimpleValueObject simpleValueObject2 = new(1);

		Assert.True(simpleValueObject1.Equals(simpleValueObject2));
		Assert.True(simpleValueObject2.Equals(simpleValueObject1));
	}

	[Fact]
	public void Equals_DeveSerVerdadeiro_ParaValoresTransitivos() {
		SimpleValueObject simpleValueObject1 = new(1);
		SimpleValueObject simpleValueObject2 = new(1);
		SimpleValueObject simpleValueObject3 = new(1);

		Assert.True(simpleValueObject1.Equals(simpleValueObject2));
		Assert.True(simpleValueObject2.Equals(simpleValueObject3));
		Assert.True(simpleValueObject1.Equals(simpleValueObject3));
	}

	[Fact]
	public void GetHashCode_DeveSerDiferente_ParaValoresDiferentes() {
		SimpleValueObject simpleValueObject1 = new(1);
		SimpleValueObject simpleValueObject2 = new(2);

		Assert.NotEqual(simpleValueObject1.GetHashCode(), simpleValueObject2.GetHashCode());
	}

	[Fact]
	public void GetHashCode_DeveSerIgual_ParaValoresIguais() {
		SimpleValueObject simpleValueObject1 = new(1);
		SimpleValueObject simpleValueObject2 = new(1);

		Assert.Equal(simpleValueObject1.GetHashCode(), simpleValueObject1.GetHashCode());
		Assert.Equal(simpleValueObject1.GetHashCode(), simpleValueObject2.GetHashCode());
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
