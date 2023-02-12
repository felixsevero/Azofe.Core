using System.ComponentModel.DataAnnotations;

namespace Azofe.Core;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
public class ScaleAttribute: ValidationAttribute {

	readonly decimal divisor;

	public ScaleAttribute(int scale) {
		if(scale < 0)
			throw new ArgumentException("The scale must be a number greater than or equal to zero.");
		divisor = scale == 0 ? 1m : 1m / (decimal)Math.Pow(10, scale);
		Scale = scale;
	}

	public int Scale { get; }

	public override bool IsValid(object? value) {
		if(value is null)
			return true;
		if(value is decimal d)
			return d % divisor == 0;
		return false;
	}

}
