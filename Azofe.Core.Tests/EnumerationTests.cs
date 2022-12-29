using Xunit;

namespace Azofe.Core.Tests;

public class EnumerationTests {

	[Fact]
	public void Construtor_DeveFalhar_ParaUmValorNulo() => Assert.Throws<ArgumentNullException>(() => new SimpleEnumeration(1, null!));

	[Fact]
	public void Equals_DeveSerFalso_ParaTiposDiferentes() => Assert.False(SimpleEnumeration.Value1.Equals(AnotherSimpleEnumeration.Value1));

	[Fact]
	public void Equals_DeveSerFalso_ParaUmValorDiferente() => Assert.False(SimpleEnumeration.Value1.Equals(SimpleEnumeration.Value2));

	[Fact]
	public void Equals_DeveSerFalso_ParaUmValorNulo() {
		SimpleEnumeration simpleEnumeration = SimpleEnumeration.Value1;

		Assert.False(simpleEnumeration.Equals(null));
		Assert.False(simpleEnumeration == null);
		Assert.False(null == simpleEnumeration);
	}

	[Fact]
	public void Equals_DeveSerVerdadeiro_ParaObjetosIguais() {
		object simpleEnumeration1 = new SimpleEnumeration(1, "Value 1");
		object simpleEnumeration2 = new SimpleEnumeration(1, "Value 1");

		// Equals method is overridden and evaluated at runtime.
		Assert.True(simpleEnumeration1.Equals(simpleEnumeration2));

		// Equality operator (==) is overloaded and evaluated at compile time.
		// See: https://stackoverflow.com/a/1849288/2406622
		Assert.False(simpleEnumeration1 == simpleEnumeration2);
	}

	[Fact]
	public void Equals_DeveSerVerdadeiro_ParaUmValorReflexivo() => Assert.True(SimpleEnumeration.Value1.Equals(SimpleEnumeration.Value1));

	[Fact]
	public void GetHashCode_DeveSerDiferente_ParaValoresDiferentes() {
		Assert.NotEqual(SimpleEnumeration.Value1.GetHashCode(), SimpleEnumeration.Value2.GetHashCode());
		Assert.NotEqual(SimpleEnumeration.Value1.GetHashCode(), AnotherSimpleEnumeration.Value1.GetHashCode());
	}

	[Fact]
	public void GetHashCode_DeveSerIgual_ParaValoresIguais() => Assert.Equal(SimpleEnumeration.Value1.GetHashCode(), SimpleEnumeration.Value1.GetHashCode());

	[Fact]
	public void GetValues_DeveRetornarUmConjunto_ParaUmaEnumeracao() {
		SimpleEnumeration[] values = Enumeration.GetValues<SimpleEnumeration>().ToArray();

		Assert.Equal(2, values.Length);
		Assert.Contains(SimpleEnumeration.Value1, values);
		Assert.Contains(SimpleEnumeration.Value2, values);
	}

	[Fact]
	public void Parse_DeveFalhar_ParaUmValorInvalido() {
		FormatException exception1 = Assert.Throws<FormatException>(() => Enumeration.Parse<SimpleEnumeration>(0));
		FormatException exception2 = Assert.Throws<FormatException>(() => Enumeration.Parse<SimpleEnumeration>("Test"));

		Assert.Equal("O valor '0' é inválido para Azofe.Core.Tests.EnumerationTests+SimpleEnumeration.", exception1.Message);
		Assert.Equal("O nome 'Test' é inválido para Azofe.Core.Tests.EnumerationTests+SimpleEnumeration.", exception2.Message);
	}

	[Fact]
	public void Parse_DeveRetornar_ParaUmValorValido() {
		SimpleEnumeration simpleEnumeration1 = Enumeration.Parse<SimpleEnumeration>(1);
		SimpleEnumeration simpleEnumeration2 = Enumeration.Parse<SimpleEnumeration>("Value 2");

		Assert.Same(SimpleEnumeration.Value1, simpleEnumeration1);
		Assert.Same(SimpleEnumeration.Value2, simpleEnumeration2);
	}

	sealed class AnotherSimpleEnumeration: Enumeration {

		public static readonly AnotherSimpleEnumeration Value1 = new(1, "Value 1");

		AnotherSimpleEnumeration(int value, string name): base(value, name) {}

	}

	sealed class SimpleEnumeration: Enumeration {

		public static readonly SimpleEnumeration Value1 = new(1, "Value 1");

		public static readonly SimpleEnumeration Value2 = new(2, "Value 2");

		public SimpleEnumeration(int value, string name): base(value, name) {}

	}

}
