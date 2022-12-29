using Xunit;

namespace Azofe.Core.Tests;

public class PagedListTests {

	[Fact]
	public void Cast_DeveRetornarUmNovoPagedList_ParaUmPagedListValido() {
		PagedList<int> pagedList = new(Enumerable.Range(1, 10).ToList(), 25, 0, 10);

		PagedList<string> result = pagedList.Cast(x => x.ToString());

		Assert.Equal(pagedList.PageItems.Count, result.PageItems.Count);
		Assert.Equal(pagedList.TotalItems, result.TotalItems);
		Assert.Equal(pagedList.PageIndex, result.PageIndex);
		Assert.Equal(pagedList.PageSize, result.PageSize);
		Assert.Equal(pagedList.TotalPages, result.TotalPages);
	}

	[Theory]
	[InlineData(-1, "O índice da página deve estar entre 0 e 2. O índice é -1.")]
	[InlineData(3, "O índice da página deve estar entre 0 e 2. O índice é 3.")]
	public void Construtor_DeveFalhar_ParaPageIndexInvalido(int pageIndex, string message) {
		ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => new PagedList<int>(Enumerable.Range(1, 10).ToList(), 28, pageIndex));

		Assert.Equal(message, exception.Message);
	}

	[Theory]
	[InlineData(8, 1, "A lista deve conter 10 itens, mas contém 8.")]
	[InlineData(5, 2, "A lista deve conter 8 itens, mas contém 5.")]
	public void Construtor_DeveFalhar_ParaPageItemsInvalido(int count, int pageIndex, string message) {
		ArgumentException exception = Assert.Throws<ArgumentException>(() => new PagedList<int>(Enumerable.Range(1, count).ToList(), 28, pageIndex));

		Assert.Equal(message, exception.Message);
	}

	[Fact]
	public void Construtor_DeveFalhar_ParaPageItemsNulo() => Assert.Throws<ArgumentNullException>(() => new PagedList<int>(null!, 28));

	[Fact]
	public void Construtor_DeveFalhar_ParaPageSizeZero() {
		ArgumentException exception = Assert.Throws<ArgumentException>(() => new PagedList<int>(Enumerable.Range(1, 10).ToList(), 28, 0, 0));

		Assert.Equal("O tamanho da página deve ser maior que zero.", exception.Message);
	}

	[Fact]
	public void Construtor_DeveFalhar_ParaTotalItemsNegativo() {
		ArgumentException exception = Assert.Throws<ArgumentException>(() => new PagedList<int>(Enumerable.Range(1, 10).ToList(), -28));

		Assert.Equal("O número total de itens deve ser maior ou igual a zero.", exception.Message);
	}

	[Theory]
	[InlineData(0, 0, 0, 15, 1)]
	[InlineData(8, 8, 0, 15, 1)]
	[InlineData(15, 15, 0, 15, 1)]
	[InlineData(15, 40, 1, 15, 3)]
	[InlineData(10, 40, 2, 15, 3)]
	public void PagedList_DeveSerCondizente_ParaValoresValidos(int count, int totalItems, int pageIndex, int pageSize, int totalPages) {
		PagedList<int> pagedList = new(Enumerable.Range(1, count).ToList(), totalItems, pageIndex, pageSize);

		Assert.Equal(count, pagedList.PageItems.Count);
		Assert.Equal(totalItems, pagedList.TotalItems);
		Assert.Equal(pageIndex, pagedList.PageIndex);
		Assert.Equal(pageSize, pagedList.PageSize);
		Assert.Equal(totalPages, pagedList.TotalPages);
	}

}
