using MediatR;

namespace Nasa.Shared.Application;

public class Command<T> : IRequest<CommandResponse<T>>
{
    public static CommandResponse<T> Success(T result)
    {
        return CommandResponse<T>.Success(result);
    }

    public static CommandResponse<T?> Fail(IEnumerable<string> errors)
    {
        return CommandResponse<T>.Fail(errors);
    }
}