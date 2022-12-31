using Xunit;

namespace Azofe.Core.Tests;

public class NotEmptyAttributeTests {

	[Theory]
	[MemberData(nameof(FalseData))]
	public void IsValid_EmptyArg_ReturnsFalse(object? value) {
		NotEmptyAttribute attribute = new();

		bool actual = attribute.IsValid(value);

		Assert.False(actual);
	}

	[Theory]
	[MemberData(nameof(TrueData))]
	public void IsValid_NotEmptyArg_ReturnsTrue(object? value) {
		NotEmptyAttribute attribute = new();

		bool actual = attribute.IsValid(value);

		Assert.True(actual);
	}

	[Fact]
	public void IsValid_WhiteSpaceString_ReturnsTrue() {
		NotEmptyAttribute attribute = new() {
			AllowWhiteSpaces = true
		};

		bool actual = attribute.IsValid(" ");

		Assert.True(actual);
	}

	public static TheoryData<object?> FalseData => new() {
		0, string.Empty, " ", new DateOnly(), new DateTime(), new TimeOnly()
	};

	public static TheoryData<object?> TrueData => new() {
		null, "Something", new DateOnly(2021, 10, 5), new DateTime(2021, 10, 5, 14, 30, 15), new TimeOnly(14, 30, 15)
	};

}
