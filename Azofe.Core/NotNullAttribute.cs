using System.ComponentModel.DataAnnotations;

namespace Azofe.Core;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
public class NotNullAttribute: ValidationAttribute {

	public override bool IsValid(object? value) => value is not null;

}
