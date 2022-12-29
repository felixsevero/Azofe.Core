namespace Azofe.Core;

public class Result {

	readonly SortedSet<ResultError> errors = new();

	public IEnumerable<ResultError> Errors => errors.ToArray();

	public bool HasErrors => errors.Count > 0;

	public static Result Success => new();

	public void AddError(string? code, string description) => errors.Add(new ResultError(code, description));

	public void AddError(ResultError error) {
		ArgumentNullException.ThrowIfNull(error);
		errors.Add(error);
	}

	public void AddErrors(IEnumerable<ResultError> errors) {
		foreach(ResultError error in errors)
			AddError(error);
	}

	public override string ToString() => $"Errors = {errors.Count}";

}

public class Result<TValue>: Result {

	TValue? value;

	public Result() {}

	public Result(TValue? value) => this.value = value;

	public TValue? Value {
		get {
			if(HasErrors)
				throw new InvalidOperationException("O valor não pode ser obtido para um objeto com erros.");
			return value;
		}
		set {
			if(HasErrors)
				throw new InvalidOperationException("O valor não pode ser definido para um objeto com erros.");
			this.value = value;
		}
	}

}

public class ResultError: ValueObject, IComparable<ResultError> {

	public ResultError(string? code, string description) {
		if(string.IsNullOrWhiteSpace(description))
			throw new ArgumentException("A descrição do erro é inválida.", nameof(description));
		Code = code;
		Description = description;
	}

	public string? Code { get; }

	public string Description { get; }

	public int CompareTo(ResultError? other) {
		if(other is null)
			return 1;
		int i = string.Compare(Code, other.Code, StringComparison.Ordinal);
		if(i != 0)
			return i;
		return string.Compare(Description, other.Description, StringComparison.Ordinal);
	}

	protected override IEnumerable<object?> GetEqualityComponents() {
		yield return Code;
		yield return Description;
	}

	public override string ToString() => $"[Code = {Code}, Description = {Description}]";

}
