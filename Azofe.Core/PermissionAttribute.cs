namespace Azofe.Core;

[AttributeUsage(AttributeTargets.Class)]
public class PermissionAttribute: Attribute {

	public const string AllowAnyone = nameof(AllowAnyone);

	public PermissionAttribute(object enumValue) {
		ArgumentNullException.ThrowIfNull(enumValue);
		if(enumValue is not Enum @enum)
			throw new ArgumentException("The object must be an enumeration.");
		Permission = GetPermission(@enum);
	}

	public PermissionAttribute(string permission) {
		ArgumentNullException.ThrowIfNull(permission);
		if(string.IsNullOrWhiteSpace(permission))
			throw new ArgumentException("The permission is invalid.");
		Permission = permission.Trim();
	}

	public string Permission { get; }

	public static string GetPermission(Enum enumValue) {
		ArgumentNullException.ThrowIfNull(enumValue);
		return $"Permission_{enumValue.GetType().Name}_{enumValue}";
	}

	public override string ToString() => Permission;

}
