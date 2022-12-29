using Xunit;

namespace Azofe.Core.Tests;

public class NotNullAttributeTests {

	[Theory]
	[MemberData(nameof(FalseData))]
	public void NotNull_DeveSerFalso_ParaUmValorNulo(object? value) {
		NotNullAttribute attribute = new();

		Assert.False(attribute.IsValid(value));
	}

	[Theory]
	[MemberData(nameof(TrueData))]
	public void NotNull_DeveSerVerdadeiro_ParaUmValorNaoNulo(object? value) {
		NotNullAttribute attribute = new();

		Assert.True(attribute.IsValid(value));
	}

	public static TheoryData<object?> FalseData => new() {
		null, new int?()
	};

	public static TheoryData<object?> TrueData => new() {
		0, string.Empty, new int?(0)
	};

}
