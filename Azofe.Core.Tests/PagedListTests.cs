using Xunit;

namespace Azofe.Core.Tests;

public class PagedListTests {

	[Fact]
	public void Cast_PagedListOfIntegers_ReturnsPagedListOfStrings() {
		PagedList<int> pagedList = new(Enumerable.Range(1, 10).ToList(), 25, 0, 10);

		PagedList<string> actual = pagedList.Cast(x => x.ToString());

		Assert.Equal(pagedList.PageItems.Count, actual.PageItems.Count);
		Assert.Equal(pagedList.TotalItems, actual.TotalItems);
		Assert.Equal(pagedList.PageIndex, actual.PageIndex);
		Assert.Equal(pagedList.PageSize, actual.PageSize);
		Assert.Equal(pagedList.TotalPages, actual.TotalPages);
	}

	[Theory]
	[InlineData(-1, "O índice da página deve estar entre 0 e 2. O índice é -1.")]
	[InlineData(3, "O índice da página deve estar entre 0 e 2. O índice é 3.")]
	public void Constructor_InvalidPageIndex_ThrowsArgumentOutOfRangeException(int pageIndex, string message) {
		Action actual = () => new PagedList<int>(Enumerable.Range(1, 10).ToList(), 28, pageIndex);

		ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(actual);
		Assert.Equal(message, exception.Message);
	}

	[Theory]
	[InlineData(8, 1, "A lista deve conter 10 itens, mas contém 8.")]
	[InlineData(5, 2, "A lista deve conter 8 itens, mas contém 5.")]
	public void Constructor_InvalidPageItems_ThrowsArgumentException(int count, int pageIndex, string message) {
		Action actual = () => new PagedList<int>(Enumerable.Range(1, count).ToList(), 28, pageIndex);

		ArgumentException exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(message, exception.Message);
	}

	[Fact]
	public void Constructor_NullPageItems_ThrowsArgumentNullException() {
		Action actual = () => new PagedList<int>(null!, 28);

		Assert.Throws<ArgumentNullException>(actual);
	}

	[Fact]
	public void Constructor_PageSizeZero_ThrowsArgumentException() {
		Action actual = () => new PagedList<int>(Enumerable.Range(1, 10).ToList(), 28, 0, 0);

		ArgumentException exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal("O tamanho da página deve ser maior que zero.", exception.Message);
	}

	[Fact]
	public void Constructor_TotalItemsLessThanZero_ThrowsArgumentException() {
		Action actual = () => new PagedList<int>(Enumerable.Range(1, 10).ToList(), -28);

		ArgumentException exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal("O número total de itens deve ser maior ou igual a zero.", exception.Message);
	}

	[Theory]
	[InlineData(0, 0, 0, 15, 1)]
	[InlineData(8, 8, 0, 15, 1)]
	[InlineData(15, 15, 0, 15, 1)]
	[InlineData(15, 40, 1, 15, 3)]
	[InlineData(10, 40, 2, 15, 3)]
	public void Constructor_ValidArgs_ExecutesSuccessfully(int count, int totalItems, int pageIndex, int pageSize, int totalPages) {
		PagedList<int> pagedList = new(Enumerable.Range(1, count).ToList(), totalItems, pageIndex, pageSize);

		Assert.Equal(count, pagedList.PageItems.Count);
		Assert.Equal(totalItems, pagedList.TotalItems);
		Assert.Equal(pageIndex, pagedList.PageIndex);
		Assert.Equal(pageSize, pagedList.PageSize);
		Assert.Equal(totalPages, pagedList.TotalPages);
	}

}
