using Xunit;

namespace Azofe.Core.Tests;

public class ScaleAttributeTests {

	[Fact]
	public void Constructor_ScaleLessThanZero_ThrowsArgumentException() {
		Action actual = () => new ScaleAttribute(-1);

		ArgumentException exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal("The scale must be a number greater than or equal to zero.", exception.Message);
	}

	[Theory]
	[MemberData(nameof(Data))]
	public void IsValid_ObjectValue_ReturnsBool(object? value, int scale, bool expected) {
		ScaleAttribute attribute = new(scale);

		bool actual = attribute.IsValid(value);

		Assert.Equal(expected, actual);
	}

	public static TheoryData<object?, int, bool> Data => new() {
		{ null, 0, true }, { 0, 0, false }, { 98.52m, 0, false }, { 98.52m, 1, false }, { 98m, 0, true }, { 98.52m, 2, true }, { 98.52m, 3, true }
	};

}
