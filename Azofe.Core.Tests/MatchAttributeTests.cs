using Xunit;

namespace Azofe.Core.Tests;

public class MatchAttributeTests {

	[Fact]
	public void Construtor_DeveFalhar_ParaUmValorIndefinido() {
		ArgumentException exception = Assert.Throws<ArgumentException>(() => new MatchAttribute((Matches)100));

		Assert.Equal("O valor é inválido.", exception.Message);
	}

	[Theory]
	[MemberData(nameof(FalseData))]
	public void Match_DeveSerFalso_ParaUmValorInvalido(object? value, Matches matches) {
		MatchAttribute attribute = new(matches);

		Assert.False(attribute.IsValid(value));
	}

	[Theory]
	[MemberData(nameof(TrueData))]
	public void Match_DeveSerVerdadeiro_ParaUmValorValido(object? value, Matches matches) {
		MatchAttribute attribute = new(matches);

		Assert.True(attribute.IsValid(value));
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
