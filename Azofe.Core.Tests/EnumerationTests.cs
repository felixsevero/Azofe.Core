using Xunit;

namespace Azofe.Core.Tests;

public class EnumerationTests {

	[Fact]
	public void Constructor_NullArg_ThrowsArgumentNullException() {
		Action actual = () => new SimpleEnumeration(1, null!);

		Assert.Throws<ArgumentNullException>(actual);
	}

	[Fact]
	public void Equals_EquivalentObject_ReturnsTrue() {
		object simpleEnumeration1 = new SimpleEnumeration(1, "Value 1");
		object simpleEnumeration2 = new SimpleEnumeration(1, "Value 1");

		bool actual = simpleEnumeration1.Equals(simpleEnumeration2);

		// Equals method is overridden and evaluated at runtime.
		Assert.True(actual);

		// Equality operator (==) is overloaded and evaluated at compile time.
		// See: https://stackoverflow.com/a/1849288/2406622
		Assert.False(simpleEnumeration1 == simpleEnumeration2);
	}

	[Fact]
	public void Equals_NotSameEnumeration_ReturnsFalse() {
		bool actual = SimpleEnumeration.Value1.Equals(AnotherSimpleEnumeration.Value1);

		Assert.False(actual);
	}

	[Fact]
	public void Equals_NotSameValue_ReturnsFalse() {
		bool actual = SimpleEnumeration.Value1.Equals(SimpleEnumeration.Value2);

		Assert.False(actual);
	}

	[Fact]
	public void Equals_NullArg_ReturnsFalse() {
		SimpleEnumeration simpleEnumeration = SimpleEnumeration.Value1;

		bool actual = simpleEnumeration.Equals(null);

		Assert.False(actual);
		Assert.False(simpleEnumeration == null);
		Assert.False(null == simpleEnumeration);
	}

	[Fact]
	public void Equals_ReflexiveValue_ReturnsTrue() {
		bool actual = SimpleEnumeration.Value1.Equals(SimpleEnumeration.Value1);

		Assert.True(actual);
	}

	[Fact]
	public void GetHashCode_NotSameValue_NotSameHash() {
		Assert.NotEqual(SimpleEnumeration.Value1.GetHashCode(), SimpleEnumeration.Value2.GetHashCode());
		Assert.NotEqual(SimpleEnumeration.Value1.GetHashCode(), AnotherSimpleEnumeration.Value1.GetHashCode());
	}

	[Fact]
	public void GetHashCode_SameValue_SameHash() => Assert.Equal(SimpleEnumeration.Value1.GetHashCode(), SimpleEnumeration.Value1.GetHashCode());

	[Fact]
	public void GetValues_Enumeration_ReturnsValues() {
		SimpleEnumeration[] values = Enumeration.GetValues<SimpleEnumeration>().ToArray();

		Assert.Equal(2, values.Length);
		Assert.Contains(SimpleEnumeration.Value1, values);
		Assert.Contains(SimpleEnumeration.Value2, values);
	}

	[Fact]
	public void Parse_InvalidArg_ThrowsFormatException() {
		Action actual1 = () => Enumeration.Parse<SimpleEnumeration>(0);
		Action actual2 = () => Enumeration.Parse<SimpleEnumeration>("Test");

		FormatException exception1 = Assert.Throws<FormatException>(actual1);
		FormatException exception2 = Assert.Throws<FormatException>(actual2);
		Assert.Equal("O valor '0' é inválido para Azofe.Core.Tests.EnumerationTests+SimpleEnumeration.", exception1.Message);
		Assert.Equal("O nome 'Test' é inválido para Azofe.Core.Tests.EnumerationTests+SimpleEnumeration.", exception2.Message);
	}

	[Fact]
	public void Parse_ValidArg_ReturnsValue() {
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
