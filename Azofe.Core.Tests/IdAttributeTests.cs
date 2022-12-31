using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Azofe.Core.Tests;

public class IdAttributeTests {

	[Theory]
	[MemberData(nameof(Data))]
	public void IsValid_ObjectValue_ReturnsBool(object? value, bool expected) {
		IdAttribute attribute = new();

		bool actual = attribute.IsValid(value);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void Validate_InvalidArg_ThrowsValidationException() {
		IdAttribute attribute = new();

		Action actual = () => attribute.Validate(false, string.Empty);

		ValidationException exception = Assert.Throws<ValidationException>(actual);
		Assert.Equal("A identidade deve ser um número maior que zero.", exception.ValidationResult.ErrorMessage);
	}

	public static TheoryData<object?, bool> Data => new() {
		{ null, true },
		{ true, false },
		{ 1, false },
		{ 1m, false },
		{ 0L, false },
		{ 1L, true }
	};

}
