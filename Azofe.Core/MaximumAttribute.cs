using System.ComponentModel.DataAnnotations;

namespace Azofe.Core;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
public class MaximumAttribute: ValidationAttribute {

	public MaximumAttribute(long maximum) => Maximum = maximum;

	public bool Exclusive { get; set; }

	public long Maximum { get; }

	bool CompareValue(decimal value) {
		if(Exclusive)
			return value < Maximum;
		return value <= Maximum;
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
