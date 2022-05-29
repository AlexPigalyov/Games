using Games.Application.Interfaces.Queries;
using OneOf;

namespace Games.Infrastructure.Interfaces.QueryHandlers;

public class QueryHandlerBase
{
    protected async Task<OneOf<TResult, QueryError<T>>> TryAsync<T, TResult>(T command, Func<T, Task<TResult>> function)
        where T : IQuery, new()
    {
        try
        {
            return await function(command);
        }
        catch (Exception e)
        {
            return new QueryError<T>
            {
                ErrorMessage = e.Message
            };
        }
    }
}