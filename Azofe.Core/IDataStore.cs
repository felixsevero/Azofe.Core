namespace Azofe.Core;

public interface IDataStore<TEntity> where TEntity: AggregateRoot {

	Task AddAsync(TEntity entity, ITransaction? transaction = default);

	Task<TEntity?> GetAsync(Id id, ITransaction? transaction = default);

	Task RemoveAsync(Id id, ConcurrencyToken concurrencyToken, ITransaction? transaction = default);

	Task SetAsync(TEntity entity, ConcurrencyToken concurrencyToken, ITransaction? transaction = default);

}
