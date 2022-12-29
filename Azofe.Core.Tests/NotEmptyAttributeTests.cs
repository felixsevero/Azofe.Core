using Xunit;

namespace Azofe.Core.Tests;

public class NotEmptyAttributeTests {

	[Theory]
	[MemberData(nameof(FalseData))]
	public void NotEmpty_DeveSerFalso_ParaUmValorVazio(object? value) {
		NotEmptyAttribute attribute = new();

		Assert.False(attribute.IsValid(value));
	}

	[Fact]
	public void NotEmpty_DeveSerVerdadeiro_ParaUmValorEmBranco() {
		NotEmptyAttribute attribute = new() {
			AllowWhiteSpaces = true
		};

		Assert.True(attribute.IsValid(" "));
	}

	[Theory]
	[MemberData(nameof(TrueData))]
	public void NotEmpty_DeveSerVerdadeiro_ParaUmValorNaoVazio(object? value) {
		NotEmptyAttribute attribute = new();

		Assert.True(attribute.IsValid(value));
	}

	public static TheoryData<object?> FalseData => new() {
		0, string.Empty, " ", new DateOnly(), new DateTime(), new TimeOnly()
	};

	public static TheoryData<object?> TrueData => new() {
		null, "Something", new DateOnly(2021, 10, 5), new DateTime(2021, 10, 5, 14, 30, 15), new TimeOnly(14, 30, 15)
	};

}
