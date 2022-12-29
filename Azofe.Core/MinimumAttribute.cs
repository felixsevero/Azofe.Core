using System.ComponentModel.DataAnnotations;

namespace Azofe.Core;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
public class MinimumAttribute: ValidationAttribute {

	public MinimumAttribute(long minimum) => Minimum = minimum;

	public bool Exclusive { get; set; }

	public long Minimum { get; }

	bool CompareValue(decimal value) {
		if(Exclusive)
			return value > Minimum;
		return value >= Minimum;
	}

	public override bool IsValid(object? value) {
		if(value is null)
			return true;
		if(value is string str)
			return CompareValue(str.Length);
		if(value is int i)
			return CompareValue(i);
		if(value is long l)
			return CompareValue(l);
		if(value is decimal d)
			return CompareValue(d);
		return false;
	}

}
