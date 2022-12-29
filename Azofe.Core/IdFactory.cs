namespace Azofe.Core;

public sealed class IdFactory: IIdFactory {

	readonly IdGen.IdGenerator generator;

	public IdFactory() {
		const int generatorId = 0;
		IdGen.DefaultTimeSource timeSource = new(new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero));
		IdGen.IdGeneratorOptions options = new(null, timeSource, IdGen.SequenceOverflowStrategy.SpinWait);
		generator = new(generatorId, options);
	}

	public Id Next() => generator.CreateId();

}
