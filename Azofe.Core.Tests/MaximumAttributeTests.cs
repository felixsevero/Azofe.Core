using Xunit;

namespace Azofe.Core.Tests;

public class MaximumAttributeTests {

	[Theory]
	[MemberData(nameof(FalseData))]
	public void Maximum_DeveSerFalso_ParaUmValorInvalido(object? value, long maximum, bool exclusive) {
		MaximumAttribute attribute = new(maximum) {
			Exclusive = exclusive
		};

		Assert.False(attribute.IsValid(value));
	}

	[Theory]
	[MemberData(nameof(TrueData))]
	public void Maximum_DeveSerVerdadeiro_ParaUmValorValido(object? value) {
		MaximumAttribute attribute = new(10);

		Assert.True(attribute.IsValid(value));
	}

	public static TheoryData<object?, long, bool> FalseData => new() {
		{ true, 0, false }, { "Something", 0, false }, { 1, 0, false }, { 1m, 0, false }, { 1L, 0L, false }, { 0, 0, true }
	};

	public static TheoryData<object?> TrueData => new() {
		null, "Something", 0, 0m, 0L
	};

}
