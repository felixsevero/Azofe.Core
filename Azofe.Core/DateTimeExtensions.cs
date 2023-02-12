using System.Globalization;

namespace Azofe.Core;

public static class DateTimeExtensions {

	const string DateFormat = "yyyy-MM-dd";
	const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";
	const string TimeFormat = "HH:mm:ss";

	public static DateOnly FromDateString(this string str) => DateOnly.ParseExact(str, DateFormat, CultureInfo.InvariantCulture);

	public static DateTime FromDateTimeString(this string str) => DateTime.ParseExact(str, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);

	public static TimeOnly FromTimeString(this string str) => TimeOnly.ParseExact(str, TimeFormat, CultureInfo.InvariantCulture);

	public static string ToDateString(this DateOnly date) => date.ToString(DateFormat, CultureInfo.InvariantCulture);

	public static string ToDateTimeString(this DateTime dateTime) {
		if(dateTime.Kind != DateTimeKind.Utc)
			throw new ArgumentException($"The conversion cannot be done. The {nameof(DateTime)} has a {dateTime.Kind} kind. A {DateTimeKind.Utc} kind is expected.");
		return dateTime.ToString(DateTimeFormat, CultureInfo.InvariantCulture);
	}

	public static string ToTimeString(this TimeOnly time) => time.ToString(TimeFormat, CultureInfo.InvariantCulture);

}
