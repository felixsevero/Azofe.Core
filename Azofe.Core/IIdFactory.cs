namespace Azofe.Core;

public interface IIdFactory {

	Id Next();

}

public interface IIdFactory<T>: IIdFactory where T: class {}
