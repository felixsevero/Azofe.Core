using Xunit;

namespace Azofe.Core.Tests;

public class MinimumAttributeTests {

	[Theory]
	[MemberData(nameof(FalseData))]
	public void IsValid_InvalidArg_ReturnsFalse(object? value, long minimum, bool exclusive) {
		MinimumAttribute attribute = new(minimum) {
			Exclusive = exclusive
		};

		bool actual = attribute.IsValid(value);

		Assert.False(actual);
	}

	[Theory]
	[MemberData(nameof(TrueData))]
	public void IsValid_ValidArg_ReturnsTrue(object? value) {
		MinimumAttribute attribute = new(0);

		bool actual = attribute.IsValid(value);

		Assert.True(actual);
	}

	public static TheoryData<object?, long, bool> FalseData => new() {
		{ true, 0, false }, { "Something", 10, false }, { 0, 1, false }, { 0m, 1, false }, { 0L, 1L, false }, { 0, 0, true }
	};

	public static TheoryData<object?> TrueData => new() {
		null, "Something", 0, 0m, 0L
	};

}
