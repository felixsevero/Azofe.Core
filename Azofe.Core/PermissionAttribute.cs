namespace Azofe.Core;

[AttributeUsage(AttributeTargets.Class)]
public class PermissionAttribute: Attribute {

	public const string AllowAnyone = nameof(AllowAnyone);

	public PermissionAttribute(object enumValue) {
		ArgumentNullException.ThrowIfNull(enumValue);
		Type enumType = enumValue.GetType();
		if(!enumType.IsEnum)
			throw new ArgumentException("The object must be an enumeration.");
		Permission = $"Permission_{enumType.Name}_{enumValue}";
	}

	public PermissionAttribute(string permission) {
		ArgumentNullException.ThrowIfNull(permission);
		if(string.IsNullOrWhiteSpace(permission))
			throw new ArgumentException("The permission is invalid.");
		Permission = permission.Trim();
	}

	public string Permission { get; }

	public override string ToString() => Permission;

}
