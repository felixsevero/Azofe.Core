namespace Azofe.Core;

public interface IDataStore<TEntity> where TEntity: AggregateRoot {

	Task<TEntity?> GetAsync(Id id, ITransaction? transaction = default);

	Task RemoveAsync(Id id, ITransaction? transaction = default);

	Task SetAsync(TEntity entity, ITransaction? transaction = default);

}
