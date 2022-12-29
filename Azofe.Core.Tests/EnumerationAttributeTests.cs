using Xunit;

namespace Azofe.Core.Tests;

public class EnumerationAttributeTests {

	[Fact]
	public void Construtor_DeveFalhar_ParaUmValorInvalido() {
		ArgumentException exception = Assert.Throws<ArgumentException>(() => new EnumerationAttribute(typeof(bool)));

		Assert.Equal("O tipo deve ser uma enumeração.", exception.Message);
	}

	[Fact]
	public void Construtor_DeveFalhar_ParaUmValorNulo() => Assert.Throws<ArgumentNullException>(() => new EnumerationAttribute(null!));

	[Theory]
	[MemberData(nameof(Data))]
	public void Enumeration_DeveSerCondizente_ParaUmValorInteiro(Type enumType, object? value, bool expected) {
		EnumerationAttribute attribute = new(enumType);

		Assert.Equal(expected, attribute.IsValid(value));
	}

	public static TheoryData<Type, object?, bool> Data => new() {
		{ typeof(SimpleEnum), null, true }, { typeof(SimpleEnum), 0, true }, { typeof(SimpleEnum), true, false }, { typeof(SimpleEnum), 1, false }, { typeof(SimpleEnumeration), 0, false }, { typeof(SimpleEnumeration), 1, true }
	};

	public enum SimpleEnum {

		SimpleValue

	}

	public sealed class SimpleEnumeration: Enumeration {

		public static readonly SimpleEnumeration ValueA = new(1, "Value A");

		SimpleEnumeration(int value, string name): base(value, name) {}

	}

}
