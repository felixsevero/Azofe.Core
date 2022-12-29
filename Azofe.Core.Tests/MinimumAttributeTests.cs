using Xunit;

namespace Azofe.Core.Tests;

public class MinimumAttributeTests {

	[Theory]
	[MemberData(nameof(FalseData))]
	public void Minimum_DeveSerFalso_ParaUmValorInvalido(object? value, long minimum, bool exclusive) {
		MinimumAttribute attribute = new(minimum) {
			Exclusive = exclusive
		};

		Assert.False(attribute.IsValid(value));
	}

	[Theory]
	[MemberData(nameof(TrueData))]
	public void Minimum_DeveSerVerdadeiro_ParaUmValorValido(object? value) {
		MinimumAttribute attribute = new(0);

		Assert.True(attribute.IsValid(value));
	}

	public static TheoryData<object?, long, bool> FalseData => new() {
		{ true, 0, false }, { "Something", 10, false }, { 0, 1, false }, { 0m, 1, false }, { 0L, 1L, false }, { 0, 0, true }
	};

	public static TheoryData<object?> TrueData => new() {
		null, "Something", 0, 0m, 0L
	};

}
