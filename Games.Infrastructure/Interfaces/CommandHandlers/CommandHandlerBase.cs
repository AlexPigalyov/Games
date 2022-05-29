using Games.Application.Interfaces.Commands;
using OneOf;

namespace Games.Infrastructure.Interfaces.CommandHandlers;

public class CommandHandlerBase
{
    protected async Task<OneOf<TResult, CommandError<T>>> TryAsync<T, TResult>(T command,
        Func<T, Task<TResult>> function)
        where T : ICommand, new()
    {
        try
        {
            return await function(command);
        }
        catch (Exception e)
        {
            return new CommandError<T>
            {
                ErrorMessage = e.Message
            };
        }
    }
}