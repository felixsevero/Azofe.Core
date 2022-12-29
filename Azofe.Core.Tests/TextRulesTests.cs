using Xunit;

namespace Azofe.Core.Tests;

public class TextRulesTests {

	[Fact]
	public void Normalize_DeveFalhar_ParaUmArgumentoNulo() => Assert.Throws<ArgumentNullException>(() => TextRules.Normalize(null!, false));

	[Theory]
	[InlineData(" ", "")]
	[InlineData(" This is my test. ", "This is my test.")]
	[InlineData("This is  my   test.", "This is my test.")]
	[InlineData("This\ris\nmy\r\ntest.", "This\nis\nmy\ntest.")]
	[InlineData("This is\u00A0my te\u00ADst.", "This ismy test.")]
	public void Normalize_DevePassar_ParaUmArgumentoValido(string value, string message) {
		string actual = TextRules.Normalize(value, true);

		Assert.Equal(message, actual);
	}

	[Theory]
	[InlineData("Test\u0000Test", "O texto é inválido, pois possui o caractere de controle 'U+0000' na posição 4 do texto.")]
	[InlineData("Test\u0100Test", "O texto deve conter apenas caracteres latinos. O caractere 'U+0100', na posição 4 do texto, é inválido.")]
	[InlineData("Test\nTest", "O texto deve estar em uma única linha. Há uma quebra de linha na posição 4 do texto.")]
	[InlineData("Test\rTest", "O texto deve estar em uma única linha. Há uma quebra de linha na posição 4 do texto.")]
	[InlineData("Test\uD83D\uDC02Test", "O texto deve conter apenas caracteres latinos. O caractere 'U+1F402', na posição 4 do texto, é inválido.")]
	public void Validate_DeveFalhar_ParaUmArgumentoInvalido(string value, string message) {
		ArgumentException exception = Assert.Throws<ArgumentException>(() => TextRules.Validate(value, false));

		Assert.Equal(message, exception.Message);
	}

	[Fact]
	public void Validate_DeveFalhar_ParaUmArgumentoNulo() => Assert.Throws<ArgumentNullException>(() => TextRules.Validate(null!, false));

	[Theory]
	[InlineData(0, 31)]
	[InlineData(127, 159)]
	public void Validate_DeveFalhar_ParaUmaSequenciaDeCaracteres(char start, char end) {
		for(char c = start; c <= end; c++)
			Assert.Throws<ArgumentException>(() => TextRules.Validate(c.ToString(), false));
	}

	[Theory]
	[InlineData("")]
	[InlineData("\r\n")]
	public void Validate_DevePassar_ParaUmArgumentoValido(string value) => TextRules.Validate(value, true);

	[Theory]
	[InlineData(32, 126)]
	[InlineData(160, 255)]
	public void Validate_DevePassar_ParaUmaSequenciaDeCaracteres(char start, char end) {
		for(char c = start; c <= end; c++)
			TextRules.Validate(c.ToString(), false);
	}

}
