using Xunit;

namespace Azofe.Core.Tests;

public class ScaleAttributeTests {

	[Fact]
	public void Construtor_DeveFalhar_ParaUmValorNegativo() {
		ArgumentException exception = Assert.Throws<ArgumentException>(() => new ScaleAttribute(-1));

		Assert.Equal("A escala deve ser maior ou igual a zero.", exception.Message);
	}

	[Theory]
	[MemberData(nameof(Data))]
	public void Scale_DeveSerCondizente_ParaUmValorDecimal(object? value, int scale, bool expected) {
		ScaleAttribute attribute = new(scale);

		Assert.Equal(expected, attribute.IsValid(value));
	}

	public static TheoryData<object?, int, bool> Data => new() {
		{ null, 0, true }, { 0, 0, false }, { 98.52m, 0, false }, { 98.52m, 1, false }, { 98m, 0, true }, { 98.52m, 2, true }, { 98.52m, 3, true }
	};

}
