using System.ComponentModel.DataAnnotations;

namespace Azofe.Core;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
public class IdAttribute: ValidationAttribute {

	public IdAttribute(): base("A identidade deve ser um número maior que zero.") {}

	public override bool IsValid(object? value) {
		if(value is null)
			return true;
		if(value is long l)
			return Id.IsValid(l);
		return false;
	}

}
