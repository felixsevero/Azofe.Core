using System.ComponentModel.DataAnnotations;

namespace Azofe.Core;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
public class IdAttribute: ValidationAttribute {

	public IdAttribute(): base("The identifier must be a number greater than zero.") {}

	public override bool IsValid(object? value) {
		if(value is null)
			return true;
		if(value is long l)
			return Id.IsValid(l);
		return false;
	}

}
