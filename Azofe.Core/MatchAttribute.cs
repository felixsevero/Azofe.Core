using System.ComponentModel.DataAnnotations;

namespace Azofe.Core;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
public class MatchAttribute: ValidationAttribute {

	public MatchAttribute(Matches matches) {
		if(!Enum.IsDefined(matches))
			throw new ArgumentException("O valor é inválido.");
		Matches = matches;
	}

	public Matches Matches { get; }

	public override bool IsValid(object? value) {
		if(value is null)
			return true;
		if(value is DateTime dateTime)
			return IsValidDateTime(dateTime);
		return false;
	}

	bool IsValidDateTime(DateTime dateTime) {
		if(Matches == Matches.DateTime)
			return dateTime.Kind == DateTimeKind.Utc;
		return false;
	}

}

public enum Matches {

	DateTime

}
