namespace Azofe.Core;

public interface ITransaction: IDisposable {

	void Complete();

}

public interface ITransactionFactory {

	ITransaction CreateTransaction();

}
