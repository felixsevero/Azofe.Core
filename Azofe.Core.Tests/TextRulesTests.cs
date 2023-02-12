using Xunit;

namespace Azofe.Core.Tests;

public class TextRulesTests {

	[Fact]
	public void Normalize_NullArg_ThrowsArgumentNullException() {
		Action actual = () => TextRules.Normalize(null!, false);

		Assert.Throws<ArgumentNullException>(actual);
	}

	[Theory]
	[InlineData(" ", "")]
	[InlineData(" This is my test. ", "This is my test.")]
	[InlineData("This is  my   test.", "This is my test.")]
	[InlineData("This\ris\nmy\r\ntest.", "This\nis\nmy\ntest.")]
	[InlineData("This is\u00A0my te\u00ADst.", "This ismy test.")]
	public void Normalize_ValidString_ReturnsNormalizedString(string value, string message) {
		string actual = TextRules.Normalize(value, true);

		Assert.Equal(message, actual);
	}

	[Theory]
	[InlineData(32, 126)]
	[InlineData(160, 255)]
	public void Validate_CharacterSequence_ExecutesSuccessfully(char start, char end) {
		for(char c = start; c <= end; c++)
			TextRules.Validate(c.ToString(), false);
	}

	[Theory]
	[InlineData(0, 31)]
	[InlineData(127, 159)]
	public void Validate_CharacterSequence_ThrowsArgumentException(char start, char end) {
		for(char c = start; c <= end; c++)
			Assert.Throws<ArgumentException>(() => TextRules.Validate(c.ToString(), false));
	}

	[Theory]
	[InlineData("Test\u0000Test", "The text is invalid. It contains the control character 'U+0000' at position 4 of the text.")]
	[InlineData("Test\u0100Test", "The text must contain only Latin characters. The character 'U+0100', at position 4 of the text, is invalid.")]
	[InlineData("Test\nTest", "The text must be on a single line. There is a line break at position 4 of the text.")]
	[InlineData("Test\rTest", "The text must be on a single line. There is a line break at position 4 of the text.")]
	[InlineData("Test\uD83D\uDC02Test", "The text must contain only Latin characters. The character 'U+1F402', at position 4 of the text, is invalid.")]
	public void Validate_InvalidString_ThrowsArgumentException(string value, string message) {
		Action actual = () => TextRules.Validate(value, false);

		ArgumentException exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(message, exception.Message);
	}

	[Fact]
	public void Validate_NullArg_ThrowsArgumentNullException() {
		Action actual = () => TextRules.Validate(null!, false);

		Assert.Throws<ArgumentNullException>(actual);
	}

	[Theory]
	[InlineData("")]
	[InlineData("\r\n")]
	public void Validate_ValidString_ExecutesSuccessfully(string value) => TextRules.Validate(value, true);

}
