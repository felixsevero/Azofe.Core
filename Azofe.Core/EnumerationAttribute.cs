using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Azofe.Core;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
public class EnumerationAttribute: ValidationAttribute {

	static readonly MethodInfo? GetValuesMethodInfo = typeof(Enumeration).GetMethod(nameof(Enumeration.GetValues));

	public EnumerationAttribute(Type enumType) {
		ArgumentNullException.ThrowIfNull(enumType);
		if(!enumType.IsEnum && !typeof(Enumeration).IsAssignableFrom(enumType))
			throw new ArgumentException("O tipo deve ser uma enumeração.");
		EnumType = enumType;
	}

	public Type EnumType { get; }

	public override bool IsValid(object? value) {
		if(value is null)
			return true;
		if(value is int i) {
			if(EnumType.IsEnum)
				return EnumType.IsEnumDefined(i);
			IEnumerable<Enumeration>? enumerable = (IEnumerable<Enumeration>?)GetValuesMethodInfo?.MakeGenericMethod(EnumType).Invoke(null, null);
			if(enumerable is null)
				throw new InvalidOperationException($"Nenhum valor foi encontrado para a enumeração {EnumType}.");
			return enumerable.Any(x => x.Value == i);
		}
		return false;
	}

}
