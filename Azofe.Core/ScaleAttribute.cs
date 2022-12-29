using System.ComponentModel.DataAnnotations;

namespace Azofe.Core;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
public class ScaleAttribute: ValidationAttribute {

	public ScaleAttribute(int scale) {
		if(scale < 0)
			throw new ArgumentException("A escala deve ser maior ou igual a zero.");
		Scale = scale;
	}

	public int Scale { get; }

	public override bool IsValid(object? value) {
		if(value is null)
			return true;
		if(value is decimal d) {
			decimal divisor = Scale == 0 ? 1m : 1m / (decimal)Math.Pow(10, Scale);
			return d % divisor == 0;
		}
		return false;
	}

}
