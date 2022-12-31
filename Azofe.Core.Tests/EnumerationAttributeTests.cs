using Xunit;

namespace Azofe.Core.Tests;

public class EnumerationAttributeTests {

	[Fact]
	public void Constructor_NotEnumeration_ThrowsArgumentException() {
		Action actual = () => new EnumerationAttribute(typeof(bool));

		ArgumentException exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal("O tipo deve ser uma enumeração.", exception.Message);
	}

	[Fact]
	public void Constructor_NullArg_ThrowsArgumentNullException() {
		Action actual = () => new EnumerationAttribute(null!);

		Assert.Throws<ArgumentNullException>(actual);
	}

	[Theory]
	[MemberData(nameof(Data))]
	public void IsValid_ObjectValue_ReturnsBool(Type enumType, object? value, bool expected) {
		EnumerationAttribute attribute = new(enumType);

		bool actual = attribute.IsValid(value);

		Assert.Equal(expected, actual);
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
