namespace Nasa.Shared.Application;

public class CommandResponse<T>
{
    private CommandResponse(bool succeeded, T result, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Result = result;
        Errors = errors;
    }

    public bool Succeeded { get; }

    public T Result { get; }

    public IEnumerable<string> Errors { get; }

    public static CommandResponse<T> Success(T result)
    {
        return new CommandResponse<T>(true, result, new List<string>());
    }

    public static CommandResponse<T?> Fail(IEnumerable<string> errors)
    {
        return new CommandResponse<T?>(false, default, errors);
    }
}