namespace Azofe.Core;

public interface IDataSource<TType> where TType: class {

	Task<TType?> GetAsync(Id id);

	Task<PagedList<TType>> GetPageAsync(int pageIndex, int pageSize);

}
