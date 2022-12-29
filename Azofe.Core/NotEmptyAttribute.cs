using System.ComponentModel.DataAnnotations;

namespace Azofe.Core;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
public class NotEmptyAttribute: ValidationAttribute {

	public bool AllowWhiteSpaces { get; set; }

	public override bool IsValid(object? value) {
		if(value is null)
			return true;
		if(value is string str)
			return IsValidString(str);
		if(value is DateOnly date)
			return date != DateOnly.MinValue;
		if(value is DateTime dateTime)
			return dateTime != DateTime.MinValue;
		if(value is TimeOnly time)
			return time != TimeOnly.MinValue;
		return false;
	}

	bool IsValidString(string value) {
		if(AllowWhiteSpaces)
			return !string.IsNullOrEmpty(value);
		return !string.IsNullOrWhiteSpace(value);
	}

}
