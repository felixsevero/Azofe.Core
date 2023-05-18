using Xunit;

namespace Azofe.Core.Tests;

public class PermissionAttributeTests {

	[Fact]
	public void Constructor_InvalidPermission_ThrowsArgumentException() {
		Action actual = () => new PermissionAttribute(" ");

		ArgumentException exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal("The permission is invalid.", exception.Message);
	}

	[Fact]
	public void Constructor_NotEnumeration_ThrowsArgumentException() {
		Action actual = () => new PermissionAttribute(false);

		ArgumentException exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal("The object must be an enumeration.", exception.Message);
	}

	[Fact]
	public void Constructor_NullArg_ThrowsArgumentNullException() {
		Action actual1 = () => new PermissionAttribute(enumValue: null!);
		Action actual2 = () => new PermissionAttribute(permission: null!);

		Assert.Throws<ArgumentNullException>(actual1);
		Assert.Throws<ArgumentNullException>(actual2);
	}

	[Fact]
	public void Constructor_ValidObject_ExecutesSuccessfully() {
		const string expected = "Permission_SimpleEnum_SimpleValue";

		PermissionAttribute attribute = new(SimpleEnum.SimpleValue);

		Assert.Equal(expected, attribute.Permission);
		Assert.Equal(expected, attribute.ToString());
	}

	[Theory]
	[InlineData(PermissionAttribute.AllowAnyone, "AllowAnyone")]
	[InlineData(" Something ", "Something")]
	public void Constructor_ValidString_ExecutesSuccessfully(string permission, string expected) {
		PermissionAttribute attribute = new(permission);

		Assert.Equal(expected, attribute.Permission);
		Assert.Equal(expected, attribute.ToString());
	}

	enum SimpleEnum {

		SimpleValue

	}

}
