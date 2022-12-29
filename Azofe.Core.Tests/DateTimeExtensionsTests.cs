using Xunit;

namespace Azofe.Core.Tests;

public class DateTimeExtensionsTests {

	[Fact]
	public void FromDateString_DeveSerIgual_ParaUmaDataEspecifica() {
		DateOnly date = new(2021, 7, 27);

		DateOnly actual = "2021-07-27".FromDateString();

		Assert.Equal(date, actual);
	}

	[Fact]
	public void FromDateTimeString_DeveSerIgual_ParaUmaDataEspecifica() {
		DateTime dateTime = new(2021, 7, 27, 17, 12, 54, DateTimeKind.Utc);

		DateTime actual = "2021-07-27T17:12:54Z".FromDateTimeString();

		Assert.Equal(dateTime, actual);
		Assert.Equal(DateTimeKind.Utc, actual.Kind);
	}

	[Fact]
	public void FromTimeString_DeveSerIgual_ParaUmaDataEspecifica() {
		TimeOnly time = new(17, 12, 54);

		TimeOnly actual = "17:12:54".FromTimeString();

		Assert.Equal(time, actual);
	}

	[Fact]
	public void ToDateString_DeveSerIgual_ParaUmaDataEspecifica() {
		DateOnly date = new(2021, 7, 27);

		Assert.Equal("2021-07-27", date.ToDateString());
	}

	[Theory]
	[MemberData(nameof(NotUtcData))]
	public void ToDateTimeString_DeveFalhar_ParaUmValorInvalido(DateTime dateTime, string message) {
		ArgumentException exception = Assert.Throws<ArgumentException>(() => dateTime.ToDateTimeString());

		Assert.Equal(message, exception.Message);
	}

	[Fact]
	public void ToDateTimeString_DeveSerIgual_ParaUmaDataEspecifica() {
		DateTime dateTime = new(2021, 7, 27, 17, 12, 54, DateTimeKind.Utc);

		Assert.Equal("2021-07-27T17:12:54Z", dateTime.ToDateTimeString());
	}

	[Fact]
	public void ToTimeString_DeveSerIgual_ParaUmaDataEspecifica() {
		TimeOnly time = new(17, 12, 54);

		Assert.Equal("17:12:54", time.ToTimeString());
	}

	public static TheoryData<DateTime, string> NotUtcData => new() {
		{ new(2021, 7, 27, 17, 12, 54, DateTimeKind.Local), "O tipo deve ser Utc. O tipo atual é Local." },
		{ new(2021, 7, 27, 17, 12, 54, DateTimeKind.Unspecified), "O tipo deve ser Utc. O tipo atual é Unspecified." }
	};

}
