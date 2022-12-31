using Xunit;

namespace Azofe.Core.Tests;

public class MatchAttributeTests {

	[Fact]
	public void Constructor_InvalidArg_ThrowsArgumentException() {
		Action actual = () => new MatchAttribute((Matches)100);

		ArgumentException exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal("O valor é inválido.", exception.Message);
	}

	[Theory]
	[MemberData(nameof(FalseData))]
	public void IsValid_InvalidArg_ReturnsFalse(object? value, Matches matches) {
		MatchAttribute attribute = new(matches);

		bool actual = attribute.IsValid(value);

		Assert.False(actual);
	}

	[Theory]
	[MemberData(nameof(TrueData))]
	public void IsValid_ValidArg_ReturnsTrue(object? value, Matches matches) {
		MatchAttribute attribute = new(matches);

		bool actual = attribute.IsValid(value);

		Assert.True(actual);
	}

	public static TheoryData<object?, Matches> FalseData => new() {
		{ true, Matches.DateTime },
		{ new DateTime(2021, 9, 7, 18, 22, 46, DateTimeKind.Local), Matches.DateTime },
		{ new DateTime(2021, 9, 7, 18, 22, 46, DateTimeKind.Unspecified), Matches.DateTime }
	};

	public static TheoryData<object?, Matches> TrueData => new() {
		{ null, Matches.DateTime },
		{ new DateTime(2021, 9, 7, 18, 22, 46, DateTimeKind.Utc), Matches.DateTime }
	};

}
