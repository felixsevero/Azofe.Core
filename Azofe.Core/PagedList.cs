using System.Collections.ObjectModel;

namespace Azofe.Core;

public class PagedList<T> {

	public PagedList(IList<T> pageItems, int totalItems): this(pageItems, totalItems, 0, 10) {}

	public PagedList(IList<T> pageItems, int totalItems, int pageIndex): this(pageItems, totalItems, pageIndex, 10) {}

	public PagedList(IList<T> pageItems, int totalItems, int pageIndex, int pageSize) {
		ArgumentNullException.ThrowIfNull(pageItems);
		if(totalItems < 0)
			throw new ArgumentException("O número total de itens deve ser maior ou igual a zero.");
		TotalItems = totalItems;
		if(pageSize <= 0)
			throw new ArgumentException("O tamanho da página deve ser maior que zero.");
		PageSize = pageSize;
		int pages = (int)Math.Ceiling(TotalItems / (double)PageSize);
		TotalPages = pages == 0 ? 1 : pages;
		if(pageIndex < 0 || pageIndex >= TotalPages)
			throw new ArgumentOutOfRangeException(message: $"O índice da página deve estar entre 0 e {TotalPages - 1}. O índice é {pageIndex}.", innerException: null);
		PageIndex = pageIndex;
		int count = PageIndex == TotalPages - 1 ? TotalItems - PageIndex * PageSize : PageSize;
		if(pageItems.Count != count)
			throw new ArgumentException($"A lista deve conter {count} itens, mas contém {pageItems.Count}.");
		PageItems = new ReadOnlyCollection<T>(pageItems);
	}

	PagedList(IList<T> pageItems, int totalItems, int pageIndex, int pageSize, int totalPages) {
		PageItems = pageItems;
		TotalItems = totalItems;
		PageIndex = pageIndex;
		PageSize = pageSize;
		TotalPages = totalPages;
	}

	public int PageIndex { get; }

	public IList<T> PageItems { get; }

	public int PageSize { get; }

	public int TotalItems { get; }

	public int TotalPages { get; }

	public PagedList<TResult> Cast<TResult>(Func<T, TResult> selector) => new(PageItems.Select(selector).ToList(), TotalItems, PageIndex, PageSize, TotalPages);

	public override string ToString() => $"Items = {PageItems.Count}";

}
