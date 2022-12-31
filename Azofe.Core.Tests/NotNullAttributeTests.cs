using Xunit;

namespace Azofe.Core.Tests;

public class NotNullAttributeTests {

	[Theory]
	[MemberData(nameof(TrueData))]
	public void IsValid_NotNullArg_ReturnsTrue(object? value) {
		NotNullAttribute attribute = new();

		bool actual = attribute.IsValid(value);

		Assert.True(actual);
	}

	[Theory]
	[MemberData(nameof(FalseData))]
	public void IsValid_NullArg_ReturnsFalse(object? value) {
		NotNullAttribute attribute = new();

		bool actual = attribute.IsValid(value);

		Assert.False(actual);
	}

	public static TheoryData<object?> FalseData => new() {
		null, new int?()
	};

	public static TheoryData<object?> TrueData => new() {
		0, string.Empty, new int?(0)
	};

}
