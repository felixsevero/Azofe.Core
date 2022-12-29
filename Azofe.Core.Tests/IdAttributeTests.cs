using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Azofe.Core.Tests;

public class IdAttributeTests {

	[Fact]
	public void Construtor_DeveDefinir_UmaMensagemDeErro() {
		IdAttribute attribute = new();

		ValidationException exception = Assert.Throws<ValidationException>(() => attribute.Validate(false, string.Empty));

		Assert.Equal("A identidade deve ser um número maior que zero.", exception.ValidationResult.ErrorMessage);
	}

	[Theory]
	[MemberData(nameof(Data))]
	public void Id_DeveSerCondizente_ParaUmValorInteiro(object? value, bool expected) {
		IdAttribute attribute = new();

		Assert.Equal(expected, attribute.IsValid(value));
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
