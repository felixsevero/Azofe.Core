namespace Azofe.Core;

public interface ITransaction: IAsyncDisposable, IDisposable {

	void Complete();

	Task CompleteAsync();

}

public interface ITransactionFactory {

	ITransaction CreateTransaction();

	Task<ITransaction> CreateTransactionAsync();

}
