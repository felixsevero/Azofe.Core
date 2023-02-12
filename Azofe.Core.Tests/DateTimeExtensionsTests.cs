using Xunit;

namespace Azofe.Core.Tests;

public class DateTimeExtensionsTests {

	[Fact]
	public void FromDateString_DateString_ReturnsDate() {
		DateOnly date = new(2021, 7, 27);

		DateOnly actual = "2021-07-27".FromDateString();

		Assert.Equal(date, actual);
	}

	[Fact]
	public void FromDateTimeString_UtcDateTimeString_ReturnsUtcDateTime() {
		DateTime dateTime = new(2021, 7, 27, 17, 12, 54, DateTimeKind.Utc);

		DateTime actual = "2021-07-27T17:12:54Z".FromDateTimeString();

		Assert.Equal(dateTime, actual);
		Assert.Equal(DateTimeKind.Utc, actual.Kind);
	}

	[Fact]
	public void FromTimeString_TimeString_ReturnsTime() {
		TimeOnly time = new(17, 12, 54);

		TimeOnly actual = "17:12:54".FromTimeString();

		Assert.Equal(time, actual);
	}

	[Fact]
	public void ToDateString_Date_ReturnsDateString() {
		DateOnly date = new(2021, 7, 27);

		string actual = date.ToDateString();

		Assert.Equal("2021-07-27", actual);
	}

	[Theory]
	[MemberData(nameof(NotUtcData))]
	public void ToDateTimeString_NotUtcDateTime_ThrowsArgumentException(DateTime dateTime, string message) {
		Action actual = () => dateTime.ToDateTimeString();

		ArgumentException exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(message, exception.Message);
	}

	[Fact]
	public void ToDateTimeString_UtcDateTime_ReturnsUtcDateTimeString() {
		DateTime dateTime = new(2021, 7, 27, 17, 12, 54, DateTimeKind.Utc);

		string actual = dateTime.ToDateTimeString();

		Assert.Equal("2021-07-27T17:12:54Z", actual);
	}

	[Fact]
	public void ToTimeString_Time_ReturnsTimeString() {
		TimeOnly time = new(17, 12, 54);

		string actual = time.ToTimeString();

		Assert.Equal("17:12:54", actual);
	}

	public static TheoryData<DateTime, string> NotUtcData => new() {
		{ new(2021, 7, 27, 17, 12, 54, DateTimeKind.Local), "The conversion cannot be done. The DateTime has a Local kind. A Utc kind is expected." },
		{ new(2021, 7, 27, 17, 12, 54, DateTimeKind.Unspecified), "The conversion cannot be done. The DateTime has a Unspecified kind. A Utc kind is expected." }
	};

}
